using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Application.Services;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Repositories;
using HeuteApp.Api.Services.Singletons;
using HeuteApp.Api.Services.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using HeuteApp.Core.Services;
using HeuteApp.Core.Commands.Dispatchers;

namespace HeuteApp.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(
            builder.Configuration.GetConnectionString("DefaultConnection"));
            
        dataSourceBuilder.EnableDynamicJson();
        var dataSource = dataSourceBuilder.Build();

        builder.Services.AddDbContext<HeuteDbContext>(options =>
        {
            options.UseNpgsql(dataSource);
        });

        // Singleton
        builder.Services.AddSingleton<SupabaseProvider>();
        builder.Services.AddSingleton<UserBasedCommandService>();

        // Scoped
        builder.Services.AddScoped<UserContext>();
        builder.Services.AddScoped<HttpClient>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
        builder.Services.AddScoped<ILayoutRepository, LayoutRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IBoardRepository, BoardRepository>();
        builder.Services.AddScoped<BoardCommandDispatcher>();
        builder.Services.AddScoped<BoardPlacementService>();

        builder.Services.AddScoped<ProfileService>();
        builder.Services.AddScoped<LayoutService>();
        builder.Services.AddScoped<CategoryService>();
        builder.Services.AddScoped<BoardService>();

        //
        builder.Services.AddScoped<UserBasedActionService>();
    }
}