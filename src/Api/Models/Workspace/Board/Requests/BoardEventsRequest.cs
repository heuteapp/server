using HeuteApp.Api.Models.Workspace.Board.Events;

namespace HeuteApp.Api.Models.Workspace.Board.Requests;

public record BoardEventsRequest(
    IEnumerable<BoardEvent> Events
);