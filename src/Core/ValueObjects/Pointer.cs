namespace HeuteApp.Core.ValueObjects;

public sealed record Pointer
{    
    public static Pointer Empty => new();

    //

    public int X { get; private set; } = 0;

    public int Y { get; private set; } = 0;

    //

    private Pointer() { }

    public Pointer(int x, int y)
    {
        X = x;
        Y = y;
    }
}