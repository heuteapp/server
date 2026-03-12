namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardKey
{    
    public DateOnly Date { get; }

    //

    private BoardKey() { }

    public BoardKey(DateOnly date)
    {
        Date = date;
    }
}