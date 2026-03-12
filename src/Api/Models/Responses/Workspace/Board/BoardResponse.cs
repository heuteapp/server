using HeuteApp.Api.Models.Responses.Workspace.Layout;

namespace HeuteApp.Api.Models.Responses.Workspace.Board;

public record BoardResponse(
    LayoutResponse? Layout,
    IEnumerable<BoardCardResponse> Cards
);