using System.Text.RegularExpressions;

namespace HeuteApp.Core.ValueObjects.Category.Path;

public sealed partial record CategoryPath
{
    [GeneratedRegex(@"^[a-zA-Z0-9-]+$")]
    private static partial Regex ValidSegmentRegex();
    
    public string Value { get; }
    private string[]? _segments;
    
    private CategoryPath(string value)
    {
        Value = value;
    }
    
    public static CategoryPath Parse(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Category path cannot be empty", nameof(path));
        
        path = path.Trim();
        
        if (path.Contains("//"))
            throw new ArgumentException("Category path cannot contain double slashes", nameof(path));
        
        if (path.StartsWith('/') || path.EndsWith('/'))
            throw new ArgumentException("Category path cannot start or end with slash", nameof(path));
        
        var segments = path.Split('/');
        
        foreach (var segment in segments)
        {
            try
            {
                ValidateSegment(segment);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Invalid segment '{segment}' in category path: {ex.Message}", nameof(path));
            }
        }
                
        return new CategoryPath(path);
    }
    
    private static void ValidateSegment(string segment)
    {        
        if (string.IsNullOrWhiteSpace(segment))
            throw new ArgumentException("Segment cannot be empty");

        if (segment.Length > 10)
            throw new ArgumentException($"Segment '{segment}' exceeds maximum length of 10 characters");

        if (!ValidSegmentRegex().IsMatch(segment))
            throw new ArgumentException($"Segment '{segment}' contains invalid characters. Only letters, numbers and hyphens are allowed");
        
        if (char.IsDigit(segment[0]))
        {
            if (YYMMDDDate.TryParse(segment, out _))
                throw new ArgumentException($"Segment '{segment}' cannot be a valid date in yyMMdd format");

            throw new ArgumentException($"Segment '{segment}' cannot start with a number");
        }
        
        if (segment.StartsWith('-') || segment.EndsWith('-'))
            throw new ArgumentException($"Segment '{segment}' cannot start or end with hyphen");   
    }
    
    public static CategoryPath FromSegments(params string[] segments)
    {
        if (segments == null || segments.Length == 0)
            throw new ArgumentException("At least one segment is required", nameof(segments));
        
        var cleanedSegments = segments
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Trim())
            .ToArray();
        
        if (cleanedSegments.Length == 0)
            throw new ArgumentException("At least one valid segment is required", nameof(segments));
        
        var path = string.Join("/", cleanedSegments);
        return Parse(path);
    }
    
    public string[] Segments => _segments ??= Value.Split('/');
    
    public string Name => Segments[^1];
    
    public bool IsChildOf(CategoryPath parent)
    {
        if (parent is null) return false;
        if (parent == this) return false;
        
        return Value.StartsWith(parent.Value + "/") && 
               Value.Length > parent.Value.Length;
    }
    
    public override string ToString() => Value;
    
    public CategoryPath? Parent
    {
        get
        {
            var segments = Segments;
            if (segments.Length <= 1)
                return null;
            
            var parentPath = string.Join("/", segments.Take(segments.Length - 1));
            return new CategoryPath(parentPath);
        }
    }
    
    public IEnumerable<CategoryPath> GetHierarchy()
    {
        var segments = Segments;
        var currentPath = segments[0];
        yield return new CategoryPath(currentPath);
        
        for (int i = 1; i < segments.Length; i++)
        {
            currentPath += "/" + segments[i];
            yield return new CategoryPath(currentPath);
        }
    }

    //

    public static CategoryPath Combine(CategoryPath parent, string child)
    {
        ArgumentNullException.ThrowIfNull(parent);

        if (string.IsNullOrWhiteSpace(child))
            throw new ArgumentException("Child segment cannot be empty", nameof(child));
        
        ValidateSegment(child);
        
        var combinedPath = $"{parent.Value}/{child}";
        return new CategoryPath(combinedPath);
    }
    
    public static CategoryPath Combine(string parent, string child)
    {
        if (string.IsNullOrWhiteSpace(parent))
            throw new ArgumentException("Parent path cannot be empty", nameof(parent));
        
        if (string.IsNullOrWhiteSpace(child))
            throw new ArgumentException("Child segment cannot be empty", nameof(child));
        
        var combinedPath = $"{parent.Trim().TrimEnd('/')}/{child.Trim()}";
        return Parse(combinedPath);
    }
    
    public CategoryPath Append(string child)
    {
        return Combine(this, child);
    }
}