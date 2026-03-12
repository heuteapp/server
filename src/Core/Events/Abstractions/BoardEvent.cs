namespace HeuteApp.Core.Events.Abstractions;

public abstract record BoardEvent(
    DateTimeOffset OccurredAt
);