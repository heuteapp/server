namespace HeuteApp.Core.ValueObjects;

public sealed record GridSize
{
    public static GridSize Empty => new();

    //

    public int ColSpan { get; private set; } = 0;

    public int RowSpan { get; private set; } = 0;

    //

    private GridSize() { }

    public GridSize(int ColSpan, int RowSpan)
    {
        this.ColSpan = ColSpan;
        this.RowSpan = RowSpan;
    }
}