namespace HeuteApp.Core.ValueObjects.Board;

public record BoardDefinition
{
    public BoardDefinition(
        BoardKey Key,
        BoardProps Props)
    {
        Date = Key.Date;
        Cards = Props.Cards;
    }

    public DateOnly Date { get; }

    public IReadOnlyCollection<BoardCardDefinition> Cards { get; }
}