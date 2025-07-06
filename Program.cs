using ChatMessAPI.Infrastructure.Helpers;
using ChatMessAPI.Infrastructure.Repositories;
using ChatMessAPI.Infrastructure.Repositories.Interfaces;
using ChatMessAPI.Services;
using ChatMessAPI.Services.Interfaces;
using DotNetEnv;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Grafana.Loki;

// Carrega primeiro o .env

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseHelper>(builder.Configuration.GetSection("DatabaseHelper"));
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();

var corsPolicy = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});
var loggerSection = builder.Configuration.GetSection("LoggerHelper");
LoggerHelper loggerHelper = loggerSection.Get<LoggerHelper>()!;

var logger = new LoggerConfiguration()
                 .MinimumLevel.Verbose()
                 .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                 .MinimumLevel.Override("System", LogEventLevel.Information)
                 .Enrich.FromLogContext()
                 .Enrich.WithProperty("app", ConstantManager.ChatMessageAPI)
                 .WriteTo.Console()
                 .WriteTo.Debug();

if(loggerHelper.MustSendLogServer)
{
    logger.WriteTo.GrafanaLoki
                    (
                       loggerHelper.UriLogServer,
                       labels: new List<LokiLabel>
                       {
                           new() { Key = "application", Value = loggerHelper.Application },
                           new() { Key = "enviroment", Value = loggerHelper.NmEnviroment},
                       },
                       textFormatter: new CustomJsonFormatter()
                    );
}
 
builder.Host.UseSerilog(logger.CreateLogger());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Host.ConfigureServices((context, services) =>
{
    services.AddControllers();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc(ConstantManager.version, new() { Title = ConstantManager.ChatMessageAPI, Version = ConstantManager.version });
    });
});
var app = builder.Build();
app.MapHealthChecks("/health"); // <--- Endpoint para checagem de health
// Edpoint para monitoramento
app.MapHealthChecks("/health-details", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        var result = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description
            })
        };
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(result);
    }
});
// Configure the HTTP request pipeline.7
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"/swagger/{ConstantManager.version}/swagger.json", $"{ConstantManager.ChatMessageAPI} {ConstantManager.version}");
    c.RoutePrefix = string.Empty; // Define a rota raiz para o Swagger
});
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(corsPolicy);
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
