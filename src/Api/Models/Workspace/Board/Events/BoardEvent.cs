namespace HeuteApp.Api.Models.Workspace.Board.Events;

public abstract record BoardEvent(
    DateTimeOffset OccurredAt
);