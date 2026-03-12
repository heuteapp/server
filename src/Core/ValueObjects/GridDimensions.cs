namespace HeuteApp.Core.ValueObjects;

public sealed record GridDimensions
{
    public static GridDimensions Empty => new();

    //

    public int ColCount { get; private set; } = 0;

    public int RowCount { get; private set; } = 0;

    //

    private GridDimensions() { }

    public GridDimensions(int colCount, int rowCount)
    {
        ColCount = colCount;
        RowCount = rowCount;
    }
}