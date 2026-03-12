namespace HeuteApp.Api.Models.Events.Abstractions;

public abstract record DomainEvent(
    DateTimeOffset OccurredAt
);