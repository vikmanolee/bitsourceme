using BitSourceMe.Core.Abstractions;
using BitSourceMe.Core.Abstractions.Models;
using BitSourceMe.Core.Clients;
using BitSourceMe.Core.Services;
using BitSourceMe.Data;
using BitSourceMe.Data.InMemory;
using BitSourceMe.Data.Sqlite;
using BitSourceMe.Data.Sqlite.Repositories;
using BitSourceMe.WebApi.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Mappers
builder.Services.AddAutoMapper(typeof(TickerPriceProfile), typeof(TickerSourceProfile));

// Add Services
builder.Services.AddSingleton<ISourceService, SourceService>();
builder.Services.AddSingleton<IPriceService, PriceService>();
builder.Services.AddSingleton<ITickerClientFactory, ClientFactory>();

// Add Clients
builder.Services.AddSingleton<ITickerClient, BitFinexClient>();
builder.Services.AddSingleton<ITickerClient, BitStampClient>();

// Add Repositories
builder.Services.AddTransient<ISourceProvider, SourceProvider>();
builder.Services.AddTransient<IPriceProvider, PriceRepository>();

builder.Services.Configure<SqliteConfiguration>(
    builder.Configuration.GetSection(SqliteConfiguration.SectionName));
builder.Services.AddSingleton<ConnectionManager>();

builder.Services.Configure<List<TickerSource>>(
    builder.Configuration.GetSection("TickerSources"));

DbSchema.Setup(builder.Configuration.GetSection(SqliteConfiguration.SectionName).Get<SqliteConfiguration>());

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
