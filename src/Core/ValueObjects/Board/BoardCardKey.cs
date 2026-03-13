namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardCardKey
{
    public static BoardCardKey Empty => new();

    //

    public string Name { get; private set; } = null!;

    //

    public BoardCardKey() { }

    public BoardCardKey(string name)
    {
        Name = name;
    }
}