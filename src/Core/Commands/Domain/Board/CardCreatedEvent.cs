using HeuteApp.Core.ValueObjects.Board;
using HeuteApp.Core.Commands.Abstractions;
using HeuteApp.Core.Enums.Commands;

namespace HeuteApp.Core.Commands.Domain.Board;

public record CreateCardCommand(
    DateTimeOffset OccurredAt,
    BoardCardDefinition Payload
) : BoardCommand(OccurredAt, BoardCommandType.CreateCard);