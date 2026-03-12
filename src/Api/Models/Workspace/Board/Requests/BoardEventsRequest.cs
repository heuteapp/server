using HeuteApp.Core.Events.Abstractions;

namespace HeuteApp.Api.Models.Workspace.Board.Requests;

public record BoardEventsRequest(
    IEnumerable<BoardEvent> Events
);