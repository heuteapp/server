using HeuteApp.Application.Interfaces.UserBased;

namespace HeuteApp.Application.Services.UserBased;

public class UserBasedActionService(
    IUserContext userContext, 
    IUserBasedActionContextFactory userBasedActionContextFactory,
    UserBasedCommandService userBasedCommandService,
    UserBasedCategoryService categoryService,
    UserBasedDailyboardService dailyboardService,
    UserBasedLayoutService layoutService)
{
    public async Task<TResult> ExecuteAsync<TResult>(Func<IUserBasedActionContext, Task<TResult>> func)
    {
        if(!userContext.UserId.HasValue){
            throw new UnauthorizedAccessException("Unauthorized: No user context found.");
        }

        var context = userBasedActionContextFactory.Create(
            userContext.UserId.Value,
            userBasedCommandService,
            categoryService,
            dailyboardService,
            layoutService
        );

        return await func(context);
    }
}