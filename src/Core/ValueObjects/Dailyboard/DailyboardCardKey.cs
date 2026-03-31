namespace HeuteApp.Core.ValueObjects.Dailyboard;

public sealed record DailyboardCardKey(string Name)
{
    public static DailyboardCardKey Empty => new();

    public DailyboardCardKey() 
        : this(string.Empty) { }
}