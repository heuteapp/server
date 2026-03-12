using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Enums.Events;

namespace HeuteApp.Core.Events.Abstractions;

public abstract record BoardEvent(
    DateTimeOffset OccurredAt,
    HeuteBoard Board,
    HeuteLayout Layout,
    BoardEventType Type
) : DomainEvent(OccurredAt);