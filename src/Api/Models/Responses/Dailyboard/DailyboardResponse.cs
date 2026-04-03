using HeuteApp.Api.Models.Responses.Layout;

namespace HeuteApp.Api.Models.Responses.Dailyboard;

public record DailyboardResponse(
    DateOnly Date,
    LayoutResponse? Layout,
    IEnumerable<DailyboardCardResponse> Cards
);