using CleanArchTemplate.Application;
using CleanArchTemplate.Infrastructure;
using CleanArchTemplate.Presentation.Endpoints;
using CleanArchTemplate.Presentation.Exceptions.ExceptionHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionStrategies();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);

var app = builder.Build();
app.MapUserEndpoints();

app.UseGlobalExceptionHandling();
app.Run();