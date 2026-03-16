using HeuteApp.Application.Services;
using HeuteApp.Application.Services.UserBased;

namespace HeuteApp.Application.Interfaces.UserBased;

public interface IUserBasedActionContext
{
    Guid UserId { get; }

    UserBasedCommandService CommandService { get; }

    CategoryService CategoryService { get; }
    
    BoardService BoardService { get; }

    LayoutService LayoutService { get; }
}
