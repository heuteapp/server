namespace HeuteApp.Core.ValueObjects;

public sealed record GridPointer
{
    public int ColIndex { get; } = 0;

    public int RowIndex { get; } = 0;

    //

    private GridPointer() { }

    public GridPointer(int ColIndex, int RowIndex)
    {
        this.ColIndex = ColIndex;
        this.RowIndex = RowIndex;
    }
}