namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardOwnership(
    Guid OwnerId,
    Guid CategoryId
);