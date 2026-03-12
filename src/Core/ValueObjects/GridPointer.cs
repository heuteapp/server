namespace HeuteApp.Core.ValueObjects;

public sealed record GridPointer
{    
    public int ColIndex { get; }

    public int RowIndex { get; }

    public GridPointer(int colIndex, int rowIndex)
    {
        ColIndex = colIndex;
        RowIndex = rowIndex;
    }
}