namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardCardDefinition(
    BoardCardKey Key,
    BoardCardProps Props
);