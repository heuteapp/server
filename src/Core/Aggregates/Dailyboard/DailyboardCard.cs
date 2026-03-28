using HeuteApp.Core.ValueObjects.Dailyboard;

namespace HeuteApp.Core.Aggregates.Dailyboard;

public class DailyboardCard
{
    protected DailyboardCard() { }

    protected DailyboardCard(DailyboardCardDefinition definition)
    {
        Id = Guid.NewGuid();
        Name = definition.Name;
        Content = definition.Content;
        Placement = definition.Placement;

        if(Placement == null)
        {
            return;
        }
    }

    public static DailyboardCard Create(DailyboardCardDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return new DailyboardCard(definition);
    }

    //

    public Guid Id { get; private set; } = Guid.Empty;

    public string Name { get; private set; } = string.Empty;

    public DailyboardCardContent Content { get; internal set; } = DailyboardCardContent.Empty;

    public DailyboardCardPlacement? Placement { get; private set; } = null;

    public bool IsPlaced => Placement is not null;
}