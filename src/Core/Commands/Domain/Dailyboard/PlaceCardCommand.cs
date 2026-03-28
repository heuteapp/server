using HeuteApp.Core.Commands.Abstractions;
using HeuteApp.Core.Commands.Payloads.Dailyboard;
using HeuteApp.Core.Enums.Commands;

namespace HeuteApp.Core.Commands.Domain.Dailyboard;

public record PlaceCardCommand(
    DateTimeOffset OccurredAt,
    PlaceCardPayload Payload
) : DailyboardCommand(OccurredAt, DailyboardCommandType.PlaceCard);