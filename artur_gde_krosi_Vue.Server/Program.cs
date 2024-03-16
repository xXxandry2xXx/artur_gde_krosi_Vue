using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using artur_gde_krosi_Vue.Server.Schedulers;
using Microsoft.EntityFrameworkCore;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using System.Text.Json.Serialization;
using Amazon.S3;
using Microsoft.AspNetCore.Identity;
using artur_gde_krosi_Vue.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using artur_gde_krosi_Vue.Server.Services.Account;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using System.Reflection.Emit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.WebHost.UseKestrel(options =>
{
    options.Limits.MaxRequestBodySize = null; // Без ограничения размера, или укажите нужный размер в байтах

});

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationIdentityContext>(options => options.UseSqlServer(connection));


builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
    options.User.RequireUniqueEmail = true;
})
    .AddDefaultTokenProviders()
 .AddEntityFrameworkStores<ApplicationIdentityContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
{
    ValidateActor = true,
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration.GetSection("JWT:Issuer").Value,
    ValidAudience = builder.Configuration.GetSection("JWT:Audience").Value,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:key").Value))
});

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IAccountValidationService, AccountValidationService>();
builder.Services.AddTransient<IAccountSetingsService, AccountSetingsService>();
builder.Services.AddTransient<IEmailService, EmailService>();

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

services.AddSingleton<ProductAndGroupJob>();
services.AddSingleton(new JobSchedule(
    jobType: typeof(ProductAndGroupJob),
    cronExpression: "0 20 0  ? * *"));
//" + ((int)DateTime.Now.Minute + 1) + " *
//   20 0

services.AddSingleton<UpdateStockJob>();
services.AddSingleton(new JobSchedule(
    jobType: typeof(UpdateStockJob),
    cronExpression: "0 0/15  * ? * *"));

services.AddAWSService<IAmazonS3>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapFallbackToFile("/index.html");
app.Run();
