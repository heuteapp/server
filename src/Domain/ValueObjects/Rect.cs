namespace HeuteApp.Domain.ValueObjects;

public readonly record struct Rect(
    int X,
    int Y,
    int Width,
    int Height)
{
    public bool Overlaps(Rect other)
    {
        return
            X < other.X + other.Width &&
            X + Width > other.X &&
            Y < other.Y + other.Height &&
            Y + Height > other.Y;
    }
}