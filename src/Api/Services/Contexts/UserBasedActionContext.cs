using HeuteApp.Api.Services.Singletons;
using HeuteApp.Application.Services;

namespace HeuteApp.Api.Services.Contexts;

public sealed record UserBasedActionContext(
    Guid UserId,
    UserBasedCommandService CommandService,
    CategoryService CategoryService,
    BoardService BoardService,
    LayoutService LayoutService
);