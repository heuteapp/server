namespace HeuteApp.Core.ValueObjects;

public sealed record GridRect
{
    public int ColIndex { get; private set;}

    public int RowIndex { get; private set;}

    public int ColSpan { get; private set;}

    public int RowSpan { get; private set;}

    //

    private GridRect() { }

    public GridRect(int ColIndex, int RowIndex, int ColSpan, int RowSpan)
    {
        this.ColIndex = ColIndex;
        this.RowIndex = RowIndex;
        this.ColSpan = ColSpan;
        this.RowSpan = RowSpan;
    }

    public GridRect(GridPointer Pointer, GridSize Size)
    {
        ColIndex = Pointer.ColIndex;
        RowIndex = Pointer.RowIndex;
        ColSpan = Size.ColSpan;
        RowSpan = Size.RowSpan;
    }

    //

    public GridPointer Pointer => new(ColIndex, RowIndex);

    public GridSize Size => new(ColSpan, RowSpan);

    //

    public bool Overlaps(GridRect other)
    {
        return
            ColIndex < other.ColIndex + other.ColSpan &&
            ColIndex + ColSpan > other.ColIndex &&
            RowIndex < other.RowIndex + other.RowSpan &&
            RowIndex + RowSpan > other.RowIndex;
    }

    public bool Contains(GridRect other)
    {
        return
            other.ColIndex >= ColIndex &&
            other.RowIndex >= RowIndex &&
            other.ColIndex + other.ColSpan <= ColIndex + ColSpan &&
            other.RowIndex + other.RowSpan <= RowIndex + RowSpan;
    }
}