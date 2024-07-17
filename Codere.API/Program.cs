using Application;
using Codere.API.Middleware;
using Domain.Interface;
using Infrastructure;
using Infrastructure.Job;
using Infrastructure.Repositories;
using log4net;
using log4net.Config;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registar servicios creados
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);


var app = builder.Build();

// Configurar log4net
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

// Configurar el pipeline de solicitud HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar ApiKeyMiddleware
app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();