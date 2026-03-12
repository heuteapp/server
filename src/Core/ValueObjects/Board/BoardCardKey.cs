namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardCardKey
{
    public string Name { get; private set; } = null!;

    //

    private BoardCardKey() { }

    public BoardCardKey(string name)
    {
        Name = name;
    }
}