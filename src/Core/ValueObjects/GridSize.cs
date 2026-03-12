namespace HeuteApp.Core.ValueObjects;

public sealed record GridSize(
    int RowCount,
    int ColCount)
{
    public bool Contains(GridRect rect)
    {
        return
            rect.ColIndex >= 1 &&
            rect.RowIndex >= 1 &&
            rect.ColIndex + rect.ColSpan - 1 <= ColCount &&
            rect.RowIndex + rect.RowSpan - 1 <= RowCount;
    }
}