namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardDefinition(
    Guid OwnerId,
    Guid LayoutId,
    BoardKey Key,
    BoardProps Props
);