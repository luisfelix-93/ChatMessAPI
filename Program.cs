using ChatMessAPI.Infrastructure.Helpers;
using ChatMessAPI.Infrastructure.Repositories;
using ChatMessAPI.Infrastructure.Repositories.Interfaces;
using ChatMessAPI.Services;
using ChatMessAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<DatabaseHelper>(builder.Configuration.GetSection("DatabaseHelper"));
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddControllers();
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
