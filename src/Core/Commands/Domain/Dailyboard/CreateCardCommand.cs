using HeuteApp.Core.Commands.Abstractions;
using HeuteApp.Core.Enums.Commands;
using HeuteApp.Core.Commands.Payloads.Dailyboard;

namespace HeuteApp.Core.Commands.Domain.Dailyboard;

public record CreateCardCommand(
    DateTimeOffset OccurredAt,
    CreateCardPayload Payload
) : DailyboardCommand(OccurredAt, DailyboardCommandType.CreateCard);