namespace HeuteApp.Core.ValueObjects;

public sealed record GridPointer
{
    public static GridPointer Empty => new();

    //

    public int ColIndex { get; private set; } = -1;

    public int RowIndex { get; private set; } = -1;

    //

    private GridPointer() { }

    public GridPointer(int ColIndex, int RowIndex)
    {
        this.ColIndex = ColIndex;
        this.RowIndex = RowIndex;
    }
}