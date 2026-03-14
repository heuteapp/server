using HeuteApp.Core.Commands.Abstractions;
using HeuteApp.Core.Commands.Payloads.Board;
using HeuteApp.Core.Enums.Commands;

namespace HeuteApp.Core.Commands.Domain.Board;

public record PlaceCardCommand(
    DateTimeOffset OccurredAt,
    PlaceCardCommandPayload Payload
) : BoardCommand(OccurredAt, BoardCommandType.PlaceCard);