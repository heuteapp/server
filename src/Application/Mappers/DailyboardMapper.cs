using HeuteApp.Application.Results.Dailyboard;
using HeuteApp.Core.Aggregates.Dailyboard;

namespace HeuteApp.Application.Mappers;

public static class DailyboardMapper
{
    public static DailyboardResult ToResult(this HeuteDailyboard dailyboard)
    {
        ArgumentNullException.ThrowIfNull(dailyboard);

        return new DailyboardResult(
            dailyboard.Id,
            dailyboard.OwnerId,
            dailyboard.LayoutId,
            dailyboard.CategoryId,
            dailyboard.Date,
            [..dailyboard.Cards.Select(ToResult)]
        );
    }

    public static DailyboardCardResult ToResult(this DailyboardCard card)
    {
        ArgumentNullException.ThrowIfNull(card);

        return new DailyboardCardResult(
            card.Id,
            card.Name,
            card.Content,
            card.Placement
        );
    }
}