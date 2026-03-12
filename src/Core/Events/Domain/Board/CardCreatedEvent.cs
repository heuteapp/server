using HeuteApp.Core.Events.Abstractions;
using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Core.Events.Domain.Board;

public record CardCreatedEvent(
    DateTimeOffset OccurredAt,
    BoardCardProps Props
) : BoardEvent(OccurredAt);