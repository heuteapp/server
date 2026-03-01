namespace HeuteApp.Core.ValueObjects;

public sealed record GridSize(
    int RowCount,
    int ColCount)
{
    public bool Contains(GridRect rect)
    {
        return
            rect.Col >= 1 &&
            rect.Row >= 1 &&
            rect.Col + rect.ColSpan - 1 <= ColCount &&
            rect.Row + rect.RowSpan - 1 <= RowCount;
    }
}