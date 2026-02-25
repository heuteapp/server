namespace HeuteApp.Domain.ValueObjects;

public readonly record struct GridRect(
    int Row,
    int Col,
    int RowSpan,
    int ColSpan)
{
    public bool Overlaps(GridRect other)
    {
        return
            Col < other.Col + other.ColSpan &&
            Col + ColSpan > other.Col &&
            Row < other.Row + other.RowSpan &&
            Row + RowSpan > other.Row;
    }
}