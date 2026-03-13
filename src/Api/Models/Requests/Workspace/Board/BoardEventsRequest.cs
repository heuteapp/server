namespace HeuteApp.Api.Models.Requests.Workspace.Board;

public record BoardEventsRequest(
    IEnumerable<BoardEventRequest> Events
);