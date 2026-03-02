using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Application.Services;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Repositories;
using Npgsql;
using HeuteApp.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var dataSourceBuilder = new NpgsqlDataSourceBuilder(
    builder.Configuration.GetConnectionString("DefaultConnection"));

dataSourceBuilder.EnableDynamicJson();

var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<HeuteDbContext>(options =>
{
    options.UseNpgsql(dataSource);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<ILayoutRepository, LayoutRepository>();
builder.Services.AddScoped<BoardPlacementService, BoardPlacementService>();
builder.Services.AddScoped<BoardService, BoardService>();
builder.Services.AddScoped<LayoutService, LayoutService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();