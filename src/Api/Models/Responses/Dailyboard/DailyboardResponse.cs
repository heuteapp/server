using HeuteApp.Api.Models.Responses.Workspace.Layout;

namespace HeuteApp.Api.Models.Responses.Workspace.Dailyboard;

public record DailyboardResponse(
    DateOnly Date,
    LayoutResponse? Layout,
    IEnumerable<DailyboardCardResponse> Cards
);