namespace HeuteApp.Core.ValueObjects;

public sealed record GridSize
{
    public int ColSpan { get; } = 0;

    public int RowSpan { get; } = 0;

    //

    private GridSize() { }

    public GridSize(int ColSpan, int RowSpan)
    {
        this.ColSpan = ColSpan;
        this.RowSpan = RowSpan;
    }
}