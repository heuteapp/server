using HeuteApp.Core.Enums.Events;

namespace HeuteApp.Api.Models.Requests.Workspace.Board;

public record BoardEventRequest(
    string OccuredAt,
    BoardEventType Type,
    object Payload
);