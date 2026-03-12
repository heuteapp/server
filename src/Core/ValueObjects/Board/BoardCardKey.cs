namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardCardKey
{
    public static BoardCardKey Empty => new();

    //

    public string Name { get; private set; } = null!;

    //

    private BoardCardKey() { }

    public BoardCardKey(string name)
    {
        Name = name;
    }
}