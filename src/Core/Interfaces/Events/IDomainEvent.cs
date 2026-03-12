namespace HeuteApp.Core.Interfaces.Events;

public interface IDomainEvent
{
    DateTimeOffset OccurredAt { get; }
}