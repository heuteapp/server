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
        
        // Check each segment for validity
        var segments = path.Split('/');
        
        foreach (var segment in segments)
        {
            ValidateSegment(segment);
        }
        
        // It should not contain any segment that looks like a date in YYMMDD format
        if (ContainsYYMMDD(segments))
            throw new ArgumentException("Category path cannot contain YYMMDD formatted segments", nameof(path));
        
        return new CategoryPath(path);
    }
    
    private static void ValidateSegment(string segment)
    {        
        // 1. Empty segment is not allowed (already caught by previous checks)
        if (string.IsNullOrWhiteSpace(segment))
            throw new ArgumentException("Segment cannot be empty");

        // 2. Special characters are not allowed (only letters, numbers and hyphens)
        if (!System.Text.RegularExpressions.Regex.IsMatch(segment, @"^[a-zA-Z0-9-]+$"))
            throw new ArgumentException($"Segment '{segment}' contains invalid characters. Only letters, numbers and hyphens are allowed");
        
        // 3. Segment length should be between 1 and 10 characters
        if (segment.Length > 10)
            throw new ArgumentException($"Segment '{segment}' exceeds maximum length of 10 characters");
        
        // 4. Segment cannot start with a number
        if (char.IsDigit(segment[0]))
            throw new ArgumentException($"Segment '{segment}' cannot start with a number");
        
        // 5. Segment cannot start or end with a hyphen
        if (segment.StartsWith('-') || segment.EndsWith('-'))
            throw new ArgumentException($"Segment '{segment}' cannot start or end with hyphen");   
    }

    private static bool ContainsYYMMDD(string[] segments)
    {
        foreach (var segment in segments)
        {
            if (IsYYMMDD(segment))
                return true;
        }
        return false;
    }
    
    private static bool IsYYMMDD(string segment)
    {
        // YYMMDD format: 6 digits, where YY is year (00-99), MM is month (01-12), DD is day (01-31)
        if (segment.Length != 6)
            return false;
        
        if (!System.Text.RegularExpressions.Regex.IsMatch(segment, @"^\d{6}$"))
            return false;
        
        // Check if it can be a valid date
        try
        {
            int year = 2000 + int.Parse(segment.Substring(0, 2));
            int month = int.Parse(segment.Substring(2, 2));
            int day = int.Parse(segment.Substring(4, 2));
            
            // Try to create a date to validate the month and day values
            var date = new DateTime(year, month, day);
            
            // Only allow years between 2000 and 2099 to avoid confusion with other formats
            return year >= 2000 && year <= 2099;
        }
        catch
        {
            return false;
        }
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