namespace HeuteApp.Core.ValueObjects.Dailyboard;

public sealed record DailyboardCardKey
{
    public static DailyboardCardKey Empty => new();

    //

    public string Name { get; private set; } = null!;

    //

    public DailyboardCardKey() { }

    public DailyboardCardKey(string name)
    {
        Name = name;
    }
}