using HeuteApp.Application.Interfaces.UserBased;
using HeuteApp.Application.Services.UserBased;

namespace HeuteApp.Api.Services.Contexts;

public sealed record UserBasedActionContext(
    Guid UserId,
    UserBasedCommandService CommandService,
    UserBasedCategoryService CategoryService,
    UserBasedBoardService BoardService,
    UserBasedLayoutService LayoutService
) : IUserBasedActionContext;