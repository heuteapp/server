namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardKey
{    
    public static BoardKey Empty => new();

    //

    public DateOnly Date { get; } = DateOnly.MinValue!;

    //

    private BoardKey() { }

    public BoardKey(DateOnly date)
    {
        Date = date;
    }
}