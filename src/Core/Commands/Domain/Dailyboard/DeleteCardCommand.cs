using HeuteApp.Core.Commands.Abstractions;
using HeuteApp.Core.Enums.Commands;
using HeuteApp.Core.Commands.Payloads.Dailyboard;

namespace HeuteApp.Core.Commands.Domain.Dailyboard;

public record DeleteCardCommand(
    DateTimeOffset OccurredAt,
    DeleteCardPayload Payload
) : DailyboardCommand(OccurredAt, DailyboardCommandType.DeleteCard);