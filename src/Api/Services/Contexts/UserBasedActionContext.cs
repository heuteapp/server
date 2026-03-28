using HeuteApp.Application.Interfaces.UserBased;
using HeuteApp.Application.Services.UserBased;

namespace HeuteApp.Api.Services.Contexts;

public sealed record UserBasedActionContext(
    Guid UserId,
    UserBasedCommandService CommandService,
    UserBasedCategoryService CategoryService,
    UserBasedDailyboardService DailyboardService,
    UserBasedLayoutService LayoutService
) : IUserBasedActionContext;

public class UserBasedActionContextFactory : IUserBasedActionContextFactory
{
    public IUserBasedActionContext Create(
        Guid userId, 
        UserBasedCommandService commandService, 
        UserBasedCategoryService categoryService, 
        UserBasedDailyboardService dailyboardService, 
        UserBasedLayoutService layoutService)
    {
        return new UserBasedActionContext(
            userId,
            commandService,
            categoryService,
            dailyboardService,
            layoutService
        );
    }
}