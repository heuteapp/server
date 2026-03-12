namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardKey
{
    public BoardKey(DateOnly date)
    {
        Date = date;
    }

    public DateOnly Date { get; }
}