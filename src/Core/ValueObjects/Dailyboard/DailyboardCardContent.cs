namespace HeuteApp.Core.ValueObjects.Dailyboard;

public record DailyboardCardContent
{
    public static DailyboardCardContent Empty => new();

    //

    public string? Title { get; private set; } = null;

    //

    public DailyboardCardContent() { }

    public DailyboardCardContent(string? title)
    {
        Title = title;
    }
}