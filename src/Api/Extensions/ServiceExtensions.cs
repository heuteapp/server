using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Application.Services;
using HeuteApp.Application.Interfaces;
using HeuteApp.Application.Interfaces.Repositories;
using HeuteApp.Infrastructure.Repositories;
using HeuteApp.Api.Services.Singletons;
using HeuteApp.Api.Services.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using HeuteApp.Core.Services;
using HeuteApp.Core.Commands.Dispatchers;
using HeuteApp.Application.Services.UserBased;
using HeuteApp.Application.Services.Internal;
using HeuteApp.Application.Interfaces.UserBased;
using HeuteApp.Application.Services.Public;

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

        // Scoped
        builder.Services.AddScoped<IUserContext, UserContext>();
        builder.Services.AddScoped<HttpClient>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
        builder.Services.AddScoped<ILayoutRepository, LayoutRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IBoardRepository, BoardRepository>();
        builder.Services.AddScoped<BoardCommandDispatcher>();
        builder.Services.AddScoped<BoardPlacementService>();

        builder.Services.AddScoped<InternalLayoutService>();
        builder.Services.AddScoped<InternalProfileService>();

        builder.Services.AddScoped<PublicProfileService>();

        builder.Services.AddScoped<UserBasedLayoutService>();
        builder.Services.AddScoped<UserBasedCategoryService>();
        builder.Services.AddScoped<UserBasedBoardService>();

        //
        builder.Services.AddScoped<UserBasedActionService>();
        builder.Services.AddScoped<UserBasedCommandService>();
        builder.Services.AddScoped<IUserBasedActionContextFactory, UserBasedActionContextFactory>();
    }
}