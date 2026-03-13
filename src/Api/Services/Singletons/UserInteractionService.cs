using HeuteApp.Api.Services.Contexts;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Services.Singletons;

public class UserBasedActionService(
    UserContext userContext,
    UserBasedCommandService userBasedCommandService,
    BoardService boardService,
    LayoutService layoutService)
{
    public async Task<IActionResult> ExecuteAsync(Func<UserBasedActionContext, Task<IActionResult>> func)
    {
        if(!userContext.UserId.HasValue){
            return new UnauthorizedObjectResult("Unauthorized: No user context found.");
        }

        var context = new UserBasedActionContext(
            userContext.UserId.Value,
            userBasedCommandService,
            boardService,
            layoutService
        );

        return await func(context);
    }
}