using System.Text.RegularExpressions;

namespace HeuteApp.Core.ValueObjects.Category;

public sealed partial record CategoryPath
{
    [GeneratedRegex(@"^[a-zA-Z0-9-]+$")]
    private static partial Regex ValidSegmentRegex();
    
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
        
        // Check each segment for validity
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
        // 1. Empty segment is not allowed (already caught by previous checks)
        if (string.IsNullOrWhiteSpace(segment))
            throw new ArgumentException("Segment cannot be empty");

        // 2. Special characters are not allowed (only letters, numbers and hyphens)
        if (!ValidSegmentRegex().IsMatch(segment))
            throw new ArgumentException($"Segment '{segment}' contains invalid characters. Only letters, numbers and hyphens are allowed");
        
        // 3. Segment length should be between 1 and 10 characters
        if (segment.Length > 10)
            throw new ArgumentException($"Segment '{segment}' exceeds maximum length of 10 characters");
        
        // 4. Segment cannot start with a number
        if (char.IsDigit(segment[0]))
        {
            // 4a. If segment looks like a date in yyMMdd format, provide specific error message
            if (YYMMDDDate.TryParse(segment, out _))
                throw new ArgumentException($"Segment '{segment}' cannot be a valid date in yyMMdd format");

            throw new ArgumentException($"Segment '{segment}' cannot start with a number");
        }
        
        // 5. Segment cannot start or end with a hyphen
        if (segment.StartsWith('-') || segment.EndsWith('-'))
            throw new ArgumentException($"Segment '{segment}' cannot start or end with hyphen");   
    }
    
    public static CategoryPath FromSegments(params string[] segments)
    {
        var cleanedSegments = segments
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Trim())
            .ToArray();
        
        var path = string.Join("/", cleanedSegments);
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