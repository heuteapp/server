namespace HeuteApp.Core.ValueObjects;

public sealed record GridDimensions
{
    public int ColCount { get; }

    public int RowCount { get; }

    public GridDimensions(int colCount, int rowCount)
    {
        ColCount = colCount;
        RowCount = rowCount;
    }
}