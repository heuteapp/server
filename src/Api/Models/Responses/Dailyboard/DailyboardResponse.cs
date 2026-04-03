using HeuteApp.Api.Models.Responses.Layout;

namespace HeuteApp.Api.Models.Responses.Dailyboard;

public record DailyboardResponse(
    string CategoryPath,
    DateOnly Date,
    LayoutResponse? Layout,
    IEnumerable<DailyboardCardResponse> Cards
);