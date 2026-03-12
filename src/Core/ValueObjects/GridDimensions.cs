namespace HeuteApp.Core.ValueObjects;

public sealed record GridDimensions
{
    public int ColCount { get; }

    public int RowCount { get; }

    //

    private GridDimensions() { }

    public GridDimensions(int colCount, int rowCount)
    {
        ColCount = colCount;
        RowCount = rowCount;
    }
}