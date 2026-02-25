namespace HeuteApp.Domain.ValueObjects;

public readonly record struct GridSize(
    int RowCount,
    int ColCount)
{
    public bool Contains(GridRect rect)
    {
        return
            rect.Col > 0 &&
            rect.Row > 0 &&
            rect.Col + rect.ColSpan <= ColCount &&
            rect.Row + rect.RowSpan <= RowCount;
    }
}