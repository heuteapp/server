namespace HeuteApp.Core.ValueObjects;

public record Chain<T>(
    T Current,
    Chain<T>? Child);