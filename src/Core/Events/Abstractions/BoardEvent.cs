using HeuteApp.Core.Enums.Events;
using HeuteApp.Core.Interfaces.Events;

namespace HeuteApp.Core.Events.Abstractions;

public abstract record BoardEvent(
    DateTimeOffset OccurredAt,
    BoardEventType Type
) : IDomainEvent;