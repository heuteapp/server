namespace HeuteApp.Core.Domain.ValueObjects;

public readonly record struct GridSize(
    int RowCount,
    int ColCount)
{
}