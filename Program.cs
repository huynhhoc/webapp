using Microsoft.EntityFrameworkCore;
using webapp;
using webapp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Move the services configuration logic to the Startup class
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Move the pipeline configuration logic to the Startup class
startup.Configure(app, builder.Environment);

app.Run();
