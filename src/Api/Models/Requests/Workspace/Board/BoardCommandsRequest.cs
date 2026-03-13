namespace HeuteApp.Api.Models.Requests.Workspace.Board;

public record BoardCommandsRequest(
    IEnumerable<BoardCommandRequest> Events
);