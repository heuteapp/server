using HeuteApp.Core.ValueObjects.Board;
using HeuteApp.Core.Events.Abstractions;
using HeuteApp.Core.Enums.Events;

namespace HeuteApp.Core.Events.Domain.Board;

public record CardCreatedEvent(
    DateTimeOffset OccurredAt,
    BoardCardDefinition Payload
) : BoardEvent(OccurredAt, BoardEventType.CardCreated);