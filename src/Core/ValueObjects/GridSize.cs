namespace HeuteApp.Core.ValueObjects;

public sealed record GridSize
{
    public int ColSpan { get; }
    
    public int RowSpan { get; }

    public GridSize(int colSpan, int rowSpan)
    {
        ColSpan = colSpan;
        RowSpan = rowSpan;
    }
}