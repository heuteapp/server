using HeuteApp.Core.Aggregates.Dailyboard;
using HeuteApp.Core.ValueObjects.Dailyboard;

namespace HeuteApp.Infrastructure.Models.Dailyboard;

public class DailyboardCardModel : DailyboardCard
{
    protected DailyboardCardModel() { }

    protected DailyboardCardModel(HeuteDailyboardModel? dailyboard, DailyboardCardDefinition definition) : base(definition)
    {
        DailyboardId = dailyboard?.Id ?? Guid.Empty;
        Dailyboard = dailyboard;
    }

    public static DailyboardCardModel Create(HeuteDailyboardModel dailyboard, DailyboardCardDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return new DailyboardCardModel(dailyboard, definition);
    }

    //

    public Guid DailyboardId { get; private set; }

    public HeuteDailyboardModel? Dailyboard { get; private set; }
}