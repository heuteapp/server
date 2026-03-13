using HeuteApp.Api.Services.Singletons;
using HeuteApp.Application.Services;

namespace HeuteApp.Api.Services.Contexts;

public sealed record UserBasedActionContext(
    Guid UserId,
    UserEventQueueService UserEventQueueService,
    BoardService BoardService,
    LayoutService LayoutService
);