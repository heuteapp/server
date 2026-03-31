namespace HeuteApp.Core.ValueObjects;

public record Hierarchy<T>(
    IReadOnlyList<Tree<T>>? Roots);