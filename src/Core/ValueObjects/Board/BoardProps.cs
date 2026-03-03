namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardProps(
    IReadOnlyCollection<BoardCardDefinition> Cards
);