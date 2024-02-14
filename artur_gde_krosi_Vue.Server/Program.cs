using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using artur_gde_krosi_Vue.Server.Schedulers;
using Microsoft.EntityFrameworkCore;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using AspNetCore.Yandex.ObjectStorage.Extensions;
using AspNetCore.Yandex.ObjectStorage.Object.Responses;
using AspNetCore.Yandex.ObjectStorage;
using Microsoft.Extensions.Options;
using AspNetCore.Yandex.ObjectStorage.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Configuration;
using Amazon.S3;
using Amazon.S3.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAuthentication("Bearer").AddJwtBearer();   

builder.WebHost.UseKestrel(options =>
{
    options.Limits.MaxRequestBodySize = null; // Без ограничения размера, или укажите нужный размер в байтах
});

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

IServiceCollection services = builder.Services;

services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Add Quartz servicesт и
services.AddHostedService<QuartzHostedService>();
services.AddSingleton<IJobFactory, SingletonJobFactory>();
services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
// Add our job
services.AddSingleton<ProductAndGroupJob>();
services.AddSingleton(new JobSchedule(
    jobType: typeof(ProductAndGroupJob),
    cronExpression: "0 " + ((int)DateTime.Now.Minute + 1) + " * ? * *"));

//" + ((int)DateTime.Now.Minute + 1) + " *
//   20 0

services.AddSingleton<UpdateStockJob>();
services.AddSingleton(new JobSchedule(
    jobType: typeof(UpdateStockJob),
    cronExpression: "0 0/15  * ? * *")); 

services.AddYandexObjectStorage(builder.Configuration.GetSection("YandexObjectStorage"));

services.AddAWSService<IAmazonS3>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.UseCors("AllowSpecificOrigin");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapFallbackToFile("/index.html");
app.Run();
