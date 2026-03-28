using HeuteApp.Core.Aggregates.Dailyboard;
using HeuteApp.Core.ValueObjects.Dailyboard;

namespace HeuteApp.Core.Mappers;

public static partial class DailyboardMapper
{
    public static DailyboardCardProps ToProps(this DailyboardCard card)
    {
        ArgumentNullException.ThrowIfNull(card);

        return new DailyboardCardProps(
            card.Content,
            card.Placement
        );
    }

    public static DailyboardCard ToDomain(this DailyboardCardDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        return DailyboardCard.Create(definition);
    }
}