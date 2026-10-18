using HomeBudgetManager.Services.Accounts.Application;
using HomeBudgetManager.Services.Accounts.Infrastructure.EventsSourcing.Kurrent;
using HomeBudgetManager.Services.Accounts.WebApi.Settings;

using Microsoft.AspNetCore.Cors.Infrastructure;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var corsSettings = CorsSettings.LoadSettings(builder.Configuration);
var kurrentDbSettings = KurrentDbSettings.LoadSettings(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    CorsSettings.SetupCors(options, corsSettings);
});
KurrentDbSettings.RegisterConnection(builder.Services, kurrentDbSettings);
builder.Services.AddApplication(c => c.WithEventsStorage<EventsStorage>());

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
