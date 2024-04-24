using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MotokaEasy.Api.Extensions;
using MotokaEasy.Core.ConstantsApp;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.ResilienceInitialization();
builder.Services.ServicesComplementaryInitialization(builder.Configuration);
builder.Services.ServicesDataBaseInitialization(builder.Configuration, builder.Environment);
builder.Services.CacheInitialization(builder.Configuration);
builder.Services.ServicesInitialization();
builder.Services.ServicesHealthChecks(builder.Configuration);
builder.Services.CloudInitialization(builder.Configuration);
builder.Services.MessageBrokerInitialization(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.ConsumersInitialization();

var app = builder.Build();

if (app.Environment.IsDevelopment()) 
    app.UseDeveloperExceptionPage(); 

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", ApplicationConstants.NameApplication); });
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

if (!app.Environment.IsDevelopment()) 
    app.UseHttpsRedirection();    

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();