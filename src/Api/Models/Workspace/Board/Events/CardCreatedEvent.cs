using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Api.Models.Workspace.Board.Events;

public record CardCreatedEvent(
    DateTimeOffset OccurredAt,
    BoardCardProps Props
) : BoardEvent(OccurredAt);