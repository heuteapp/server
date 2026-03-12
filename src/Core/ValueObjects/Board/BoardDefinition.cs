namespace HeuteApp.Core.ValueObjects.Board;

public record BoardDefinition
{
    public BoardDefinition(
        DateOnly Date,
        IReadOnlyCollection<BoardCardDefinition> Cards)
    {
        this.Date = Date;
        this.Cards = Cards;
    }

    public BoardDefinition(
        BoardKey Key,
        BoardProps Props)
    {
        Date = Key.Date;
        Cards = Props.Cards;
    }

    //

    public DateOnly Date { get; }

    public IReadOnlyCollection<BoardCardDefinition> Cards { get; }
}