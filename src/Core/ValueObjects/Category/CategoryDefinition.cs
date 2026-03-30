using System.Text.Json.Serialization;

namespace HeuteApp.Core.ValueObjects.Category;

public sealed record CategoryDefinition
{
    public static CategoryDefinition Empty => new();

    //

    public string Name { get; private set; } = null!;

    //

    private CategoryDefinition() { }

    public CategoryDefinition(
        string name)
    {
        Name = name;
    }

    public CategoryDefinition(
        CategoryKey key,
        CategoryProps props)
    {
        Name = key.Name;
    }

    //
    
    [JsonIgnore]
    public CategoryKey Key => new(Name);
    
    [JsonIgnore]
    public CategoryProps Props => CategoryProps.Empty;
}