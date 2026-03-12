namespace HeuteApp.Core.ValueObjects.Board;

public record BoardDefinition
{
    public BoardDefinition(
        DateOnly date)
    {
        Date = date;
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