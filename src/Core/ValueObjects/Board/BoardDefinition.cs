namespace HeuteApp.Core.ValueObjects.Board;

public record BoardDefinition
{
    public BoardDefinition(
        DateOnly Date)
    {
        this.Date = Date;
    }

    public BoardDefinition(
        BoardKey Key,
        BoardProps Props)
    {
        Date = Key.Date;
    }

    //

    public DateOnly Date { get; }
}