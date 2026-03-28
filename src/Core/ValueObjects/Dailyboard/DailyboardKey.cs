namespace HeuteApp.Core.ValueObjects.Dailyboard;

public sealed record DailyboardKey
{    
    public static DailyboardKey Empty => new();

    //

    public DateOnly Date { get; private set; } = DateOnly.MinValue!;

    //

    public DailyboardKey() { }

    public DailyboardKey(DateOnly date)
    {
        Date = date;
    }
}