namespace HeuteApp.Core.ValueObjects;

public sealed record Pointer
{    
    public int X { get; }

    public int Y { get; }

    public Pointer(int x, int y)
    {
        X = x;
        Y = y;
    }
}