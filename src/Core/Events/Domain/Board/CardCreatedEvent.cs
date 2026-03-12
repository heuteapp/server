using HeuteApp.Core.ValueObjects.Board;
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Events.Abstractions;
using HeuteApp.Core.Enums.Events;

namespace HeuteApp.Core.Events.Domain.Board;

public record CardCreatedEvent(
    DateTimeOffset OccurredAt,
    HeuteBoard Board,
    HeuteLayout Layout,
    BoardCardDefinition Definition
) : BoardEvent(OccurredAt, Board, Layout, BoardEventType.CardCreated);