using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;

namespace HeuteApp.Core.Events.Abstractions;

public abstract record BoardEvent(
    DateTimeOffset OccurredAt,
    HeuteBoard Board,
    HeuteLayout Layout,
    BoardEventType Type
) : DomainEvent(OccurredAt);

public enum BoardEventType
{
    CardCreated,
}