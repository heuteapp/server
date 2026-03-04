namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardReference(
    Guid OwnerId,
    Guid CategoryId
);