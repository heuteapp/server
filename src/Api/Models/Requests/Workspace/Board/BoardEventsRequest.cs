using HeuteApp.Core.Events.Abstractions;

namespace HeuteApp.Api.Models.Requests.Workspace.Board;

public record BoardEventsRequest(
    IEnumerable<BoardEvent> Events
);