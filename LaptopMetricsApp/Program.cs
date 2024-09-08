using System;
using System.Diagnostics;
using System.Threading;
using Prometheus;
using SystemMonitoringApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();

//Register Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddControllers();

builder.WebHost.UseUrls("http://0.0.0.0:9100"); 

// CPU Metrics Collection Thread
static void CpuThread()
{
    while (true)
    {
        GetMetrics.GetCPUUsage();
        GetMetrics.GetRAMUsage();
        GetMetrics.GetDiskUsage();
        GetMetrics.GetNetworkData();
        
        // Add a sleep interval to reduce CPU usage
        Thread.Sleep(1000);
    }
}

// Start the CPU metrics collection thread
Thread thread = new Thread(new ThreadStart(CpuThread));
thread.Start();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseHttpMetrics();   // Adds Prometheus metrics tracking to HTTP requests
app.MapMetrics();       // Exposes the /metrics endpoint for Prometheus scraping
app.MapControllers();   // Maps controller routes

app.MapPost("/metrics", () =>
{
    LaptopMetrics laptopMetrics = new LaptopMetrics(
        GetMetrics.GetCPUUsage(),
        GetMetrics.GetRAMUsage(),
        GetMetrics.GetNetworkData(),
        GetMetrics.GetDiskUsage()
    );

    return laptopMetrics;
})
.WithName("metrics")
.WithOpenApi();

app.Run();

// Record type to hold laptop metrics data
record LaptopMetrics(float CpuUsage, float RamUsage, GetMetrics.NetworkData[] NetworkData, GetMetrics.DiskInfo[] DiskInfo);
