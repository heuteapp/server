namespace HeuteApp.Core.ValueObjects.Dailyboard;

public record DailyboardDefinition
{   
    public static DailyboardDefinition Empty => new();

    //

    public DateOnly Date { get; private set; } = DateOnly.MinValue;

    //

    public DailyboardDefinition() { }

    public DailyboardDefinition(
        DateOnly date)
    {
        Date = date;
    }

    public DailyboardDefinition(
        DailyboardKey Key,
        DailyboardProps Props)
    {
        Date = Key.Date;
    }
}