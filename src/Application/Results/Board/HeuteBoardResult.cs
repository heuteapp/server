namespace HeuteApp.Application.Results.Board;

public sealed record BoardResult(
    Guid Id,
    Guid OwnerId,
    Guid LayoutId,
    Guid CategoryId,
    DateOnly Date,
    IReadOnlyCollection<BoardCardResult> Cards);