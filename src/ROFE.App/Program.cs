using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ROFE.App.Extensions;
using System;


var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();

Console.WriteLine("Env:" + builder.Environment.EnvironmentName);

IConfiguration configuration = configurationBuilder.Build();
configuration.Configure();

builder.Services.Configure();

var app = builder.Build();
app.Configure();
app.Run();
