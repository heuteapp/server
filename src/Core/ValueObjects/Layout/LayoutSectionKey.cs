namespace HeuteApp.Core.ValueObjects.Layout;

public sealed record LayoutSectionKey(string Name)
{
    public static LayoutSectionKey Empty => new("");
}