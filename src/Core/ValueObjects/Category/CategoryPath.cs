// src/Core/ValueObjects/Category/CategoryPath.cs
namespace HeuteApp.Core.ValueObjects.Category;

public sealed record CategoryPath
{
    public string Value { get; }
    
    private CategoryPath(string value)
    {
        Value = value;
    }
    
    public static CategoryPath Create(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Category path cannot be empty", nameof(path));
        
        if (path.Contains("//"))
            throw new ArgumentException("Category path cannot contain double slashes", nameof(path));
        
        if (path.StartsWith('/') || path.EndsWith('/'))
            throw new ArgumentException("Category path cannot start or end with slash", nameof(path));
        
        return new CategoryPath(path);
    }
    
    public static CategoryPath FromSegments(params string[] segments)
    {
        var path = string.Join("/", segments.Where(s => !string.IsNullOrWhiteSpace(s)));
        return Create(path);
    }
    
    public string[] Segments => Value.Split('/');
    
    public CategoryPath? Parent
    {
        get
        {
            var segments = Segments;
            if (segments.Length <= 1)
                return null;
            
            var parentPath = string.Join("/", segments.Take(segments.Length - 1));
            return Create(parentPath);
        }
    }
    
    public string Name => Segments[^1];
    
    public bool IsChildOf(CategoryPath parent)
    {
        return Value.StartsWith(parent.Value + "/");
    }
    
    public override string ToString() => Value;
}