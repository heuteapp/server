using HeuteApp.Core.ValueObjects.Dailyboard;

namespace HeuteApp.Application.Results.Dailyboard;

public sealed record DailyboardCardResult(
    Guid Id,
    string Name,
    DailyboardCardContent? Content,
    DailyboardCardPlacement? Placement);