namespace HeuteApp.Core.ValueObjects;

public sealed record Size
{
    public int Width { get; }

    public int Height { get; }

    //

    private Size() { }

    public Size(int width, int height)
    {
        Width = width;
        Height = height;
    }
}