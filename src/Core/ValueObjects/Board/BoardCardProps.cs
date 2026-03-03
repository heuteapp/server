namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardCardProps(
    string? Title,
    BoardCardPlacement? Placement
);