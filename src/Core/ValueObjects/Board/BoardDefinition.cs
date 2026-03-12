namespace HeuteApp.Core.ValueObjects.Board;

public record BoardDefinition
{   
    public static BoardDefinition Empty => new();

    //

    public DateOnly Date { get; } = DateOnly.MinValue;

    //

    private BoardDefinition() { }

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