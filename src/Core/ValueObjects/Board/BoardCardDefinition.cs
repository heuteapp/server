namespace HeuteApp.Core.ValueObjects.Board;

public record BoardCardDefinition
{
    public BoardCardDefinition(
        BoardCardKey Key,
        BoardCardProps Props)
    {
        Name = Key.Name;
        Content = Props.Content;
        Placement = Props.Placement;
    }

    public string Name { get; }

    public BoardCardContent Content { get; }

    public BoardCardPlacement? Placement { get; }
}