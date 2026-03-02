namespace HeuteApp.Application.Results.Board;

public sealed record HeuteBoardResult(
    Guid Id,
    Guid OwnerId,
    Guid LayoutId,
    DateOnly Date,
    IReadOnlyCollection<BoardCardResult> Cards
);