namespace HeuteApp.Core.ValueObjects.Dailyboard;

public sealed record DailyboardKey(DateOnly Date)
{
    public static DailyboardKey Empty => new();

    public DailyboardKey() 
        : this(DateOnly.MinValue) { }
}