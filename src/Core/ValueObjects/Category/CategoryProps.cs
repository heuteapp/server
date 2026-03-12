namespace HeuteApp.Core.ValueObjects.Category;

public sealed record CategoryProps
{
    public static CategoryProps Empty => new();

    //

    private CategoryProps() { }
}