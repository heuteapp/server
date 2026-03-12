namespace HeuteApp.Api.Models.Responses.Workspace.Board;

public record BoardResponse(
    string LayoutName,
    int? LayoutVersion,
    IEnumerable<BoardCardResponse> Cards
);