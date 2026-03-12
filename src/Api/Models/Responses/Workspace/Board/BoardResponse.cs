using HeuteApp.Api.Models.Responses.Workspace.Layout;

namespace HeuteApp.Api.Models.Responses.Workspace.Board;

public record BoardResponse(
    DateOnly Date,
    LayoutResponse? Layout,
    IEnumerable<BoardCardResponse> Cards
);