using HeuteApp.Core.Enums.Events;

namespace HeuteApp.Api.Models.Requests.Workspace.Board;

public record BoardEventRequest(
    string OccurredAt,
    BoardEventType Type,
    object Payload
);