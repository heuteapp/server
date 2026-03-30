namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutKey(string Name, int Version)
{
    public static LayoutKey Empty => new(string.Empty, 0);
}