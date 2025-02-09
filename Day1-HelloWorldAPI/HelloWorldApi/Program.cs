using HelloWorldApi.Models;
using HelloWorldApi.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.PostgreSQL;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default");

//Add Serilog stuffs
var columnWriters = new Dictionary<string, ColumnWriterBase>
{
    {"timestamp", new TimestampColumnWriter() },
    {"level", new LevelColumnWriter() },
    {"message", new RenderedMessageColumnWriter() },
    {"exception", new ExceptionColumnWriter() },
    {"log_event", new LogEventSerializedColumnWriter() },
};

Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.PostgreSQL(connectionString, tableName: "Logs", columnOptions: columnWriters, needAutoCreateTable: true).CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Add services to the container.
builder.Services.AddScoped<ToDoService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(); // Explicitly add Swagger

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enables Swagger JSON
    app.UseSwaggerUI(); // Enables Swagger UI
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
