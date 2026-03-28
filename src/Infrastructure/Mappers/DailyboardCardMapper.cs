using HeuteApp.Core.ValueObjects.Dailyboard;
using HeuteApp.Infrastructure.Models.Dailyboard;

namespace HeuteApp.Infrastructure.Mappers;

public static class DailyboardCardMapper
{
    public static DailyboardCardProps ToProps(this DailyboardCardModel model)
    {
        return new DailyboardCardProps(
            model.Content,
            model.Placement
        );
    }
}