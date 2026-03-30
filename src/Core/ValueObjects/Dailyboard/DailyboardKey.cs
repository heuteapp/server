namespace HeuteApp.Core.ValueObjects.Dailyboard;

public sealed record DailyboardKey(DateOnly Date)
{
    public static DailyboardKey Empty => new(DateOnly.MinValue);
}