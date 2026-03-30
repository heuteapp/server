namespace HeuteApp.Core.ValueObjects.Dailyboard;

public sealed record DailyboardCardContent(string? Title)
{
    public static DailyboardCardContent Empty => new(Title:null);
}