using HeuteApp.Api.Services.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Services.Singletons;

public class UserBasedActionService(
    UserContext userContext)
{
    public async Task<IActionResult> Execute(Func<Guid, Task<IActionResult>> func)
    {
        if(!userContext.UserId.HasValue){
            return new UnauthorizedObjectResult("Unauthorized: No user context found.");
        }

        return await func(userContext.UserId.Value);
    }
}