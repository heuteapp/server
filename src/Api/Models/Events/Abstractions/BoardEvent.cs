namespace HeuteApp.Api.Models.Events.Abstractions;


public abstract record BoardEvent(
    DateTimeOffset OccurredAt,
    BoardEventType Type
) : DomainEvent(OccurredAt);

public enum BoardEventType
{
    CardCreated,
}