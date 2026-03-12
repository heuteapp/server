using HeuteApp.Core.Interfaces.Events;

namespace HeuteApp.Api.Models.Events.Abstractions;


public abstract record BoardEvent(
    DateTimeOffset OccurredAt,
    BoardEventType Type
) : IDomainEvent;

public enum BoardEventType
{
    CardCreated,
}