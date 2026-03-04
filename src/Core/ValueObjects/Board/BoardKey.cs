namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardKey(
    string Category,
    DateOnly Date
);