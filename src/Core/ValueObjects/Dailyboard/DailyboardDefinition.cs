namespace HeuteApp.Core.ValueObjects.Dailyboard;

public sealed record DailyboardDefinition(DateOnly Date)
{
    public static DailyboardDefinition Empty => new();

    //

    public DailyboardDefinition() 
        : this(DateOnly.MinValue) { }

    public DailyboardDefinition(DailyboardKey key)
        : this(key.Date) { }
}