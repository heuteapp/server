namespace HeuteApp.Core.Events.Abstractions;

public abstract record DomainEvent(
    DateTimeOffset OccurredAt
);