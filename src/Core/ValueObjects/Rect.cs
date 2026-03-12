namespace HeuteApp.Core.ValueObjects;

public sealed record Rect
{    
    public static Rect Empty => new();

    //

    public int X { get; private set; } = 0;

    public int Y { get; private set; } = 0;

    public int Width { get; private set; } = 0;

    public int Height { get; private set; } = 0;

    //

    private Rect() { }

    public Rect(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
}