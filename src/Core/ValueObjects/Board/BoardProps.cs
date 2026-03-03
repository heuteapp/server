namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardProps
{
    public IReadOnlyCollection<BoardCardDefinition> Cards { get; }

    public BoardProps(IEnumerable<BoardCardDefinition> cards)
    {
        Cards = cards.ToList().AsReadOnly();
    }
}