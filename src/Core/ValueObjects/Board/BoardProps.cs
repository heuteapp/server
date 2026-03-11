namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardProps(
    IReadOnlyCollection<BoardCardDefinition> Cards
)
{
    public static readonly BoardProps Empty = new([]);
}