namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardCardProps(
    BoardCardContent Content,
    BoardCardPlacement? Placement
);