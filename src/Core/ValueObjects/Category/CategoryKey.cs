namespace HeuteApp.Core.ValueObjects.Category;

public sealed record CategoryKey(string Name)
{
    public static CategoryKey Empty => new("");
}