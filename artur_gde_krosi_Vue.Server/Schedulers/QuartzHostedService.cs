using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using artur_gde_krosi_Vue.Server.Schedulers;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;

namespace artur_gde_krosi_Vue.Server.Schedulers
{
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<JobSchedule> _jobSchedules;

        public QuartzHostedService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            IEnumerable<JobSchedule> jobSchedules)
        {
            _schedulerFactory = schedulerFactory;
            _jobSchedules = jobSchedules;
            _jobFactory = jobFactory;
        }
        public IScheduler Scheduler { get; set; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            foreach (var jobSchedule in _jobSchedules)
            {
                var jobKey = new JobKey(jobSchedule.JobType.FullName);

                if (await Scheduler.CheckExists(jobKey, cancellationToken))
                {
                    // Логируйте факт существования задачи
                    Console.WriteLine($"Job with key {jobKey} already exists. Creating a new one.");

                    // Создайте новый уникальный идентификатор (в данном случае, добавив GUID)
                    jobKey = new JobKey($"{jobKey.Name}_{Guid.NewGuid()}", jobKey.Group);
                }

                var job = CreateJob(jobSchedule);
                var trigger = CreateTrigger(jobSchedule);


                await Scheduler.ScheduleJob((Quartz.IJobDetail)job, trigger, cancellationToken);
            }

            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        private static IJobDetail CreateJob(JobSchedule schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();
        }

        private static ITrigger CreateTrigger(JobSchedule schedule)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.JobType.FullName}.trigger")
                .WithCronSchedule(schedule.CronExpression)
                .WithDescription(schedule.CronExpression)
                .Build();
        }
    }
}