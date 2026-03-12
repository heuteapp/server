using HeuteApp.Core.ValueObjects.Board;
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Events.Abstractions;

namespace HeuteApp.Core.Events.Domain.Board;

public record CardCreatedEvent(
    DateTimeOffset OccurredAt,
    HeuteBoard Board,
    HeuteLayout Layout,
    BoardCardDefinition Definition
) : BoardEvent(OccurredAt, Board, Layout, BoardEventType.CardCreated);