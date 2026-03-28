namespace HeuteApp.Application.Results.Dailyboard;

public sealed record DailyboardResult(
    Guid Id,
    Guid OwnerId,
    Guid LayoutId,
    Guid CategoryId,
    DateOnly Date,
    IReadOnlyCollection<DailyboardCardResult> Cards);