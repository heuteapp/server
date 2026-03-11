using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Application.Services;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Repositories;
using Npgsql;
using HeuteApp.Core.Services;
using HeuteApp.Api.Singletons;

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

builder.Services.AddSingleton<SupabaseProvider>();

builder.Services.AddScoped<HttpClient>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<ILayoutRepository, LayoutRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<BoardPlacementService>();

builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<LayoutService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<BoardService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


var app = builder.Build();
app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();