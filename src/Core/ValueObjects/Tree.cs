namespace HeuteApp.Core.ValueObjects;

public record Tree<T>(
    T Current,
    IReadOnlyList<Tree<T>>? Children);