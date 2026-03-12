namespace HeuteApp.Api.Models.Responses.Workspace.Board;

public record BoardResponse(
    IEnumerable<BoardCardResponse> Cards
);