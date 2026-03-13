using HeuteApp.Core.Enums.Commands;

namespace HeuteApp.Api.Models.Requests.Workspace.Board;

public record BoardCommandRequest(
    string OccurredAt,
    BoardCommandType Type,
    object Payload
);