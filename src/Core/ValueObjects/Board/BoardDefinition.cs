namespace HeuteApp.Core.ValueObjects.Board;

public record BoardDefinition
{   
    public static BoardDefinition Empty => new();

    //

    public DateOnly Date { get; private set; } = DateOnly.MinValue;

    //

    public BoardDefinition() { }

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
}