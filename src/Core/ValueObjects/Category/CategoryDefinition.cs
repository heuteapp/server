using System.Text.Json.Serialization;

namespace HeuteApp.Core.ValueObjects.Category;

public sealed record CategoryDefinition(string Name)
{
    public static CategoryDefinition Empty => new();

    //

    public CategoryDefinition() 
        : this(string.Empty) { }

    public CategoryDefinition(CategoryKey key)
        : this(key.Name) { }

    //

    [JsonIgnore]
    public CategoryKey Key => new(Name);
}