using VehicleManagementSystem.Api.Middleware;
using VehicleManagementSystem.Application.Commission;
using VehicleManagementSystem.Application.Commission.Strategies;
using VehicleManagementSystem.Application.Services;
using VehicleManagementSystem.Core.Interfaces;
using VehicleManagementSystem.Infrastructure.Data;
using VehicleManagementSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DbConnectionFactory>();

// Repository (Stateful Singleton so RAM database state persists across HTTP requests!)
builder.Services.AddSingleton<ICarModelRepository, CarModelRepository>();

// Commission Strategies
builder.Services.AddSingleton<IBrandCommissionStrategy, AudiCommissionStrategy>();
builder.Services.AddSingleton<IBrandCommissionStrategy, JaguarCommissionStrategy>();
builder.Services.AddSingleton<IBrandCommissionStrategy, LandRoverCommissionStrategy>();
builder.Services.AddSingleton<IBrandCommissionStrategy, RenaultCommissionStrategy>();

// Commission Factory
builder.Services.AddSingleton<CommissionStrategyFactory>();

// Application Services
builder.Services.AddScoped<ICarModelService, CarModelService>();
builder.Services.AddScoped<ICommissionService, CommissionService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = builder.Configuration["Jwt:Issuer"],
            ValidAudience            = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey         = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
    opt.AddPolicy("AngularApp", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AngularApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.Run();
