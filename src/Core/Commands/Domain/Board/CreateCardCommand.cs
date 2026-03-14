using HeuteApp.Core.Commands.Abstractions;
using HeuteApp.Core.Enums.Commands;
using HeuteApp.Core.Commands.Payloads.Board;

namespace HeuteApp.Core.Commands.Domain.Board;

public record CreateCardCommand(
    DateTimeOffset OccurredAt,
    CreateCardPayload Payload
) : BoardCommand(OccurredAt, BoardCommandType.CreateCard);