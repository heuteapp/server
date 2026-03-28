using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Core.ValueObjects.Dailyboard;

public sealed partial record DailyboardPath
{
    public CategoryPath CategoryPath { get; }
    public YYMMDDDate? Date { get; }
    
    private DailyboardPath(CategoryPath categoryPath, YYMMDDDate? date = null)
    {
        CategoryPath = categoryPath ?? throw new ArgumentNullException(nameof(categoryPath));
        Date = date;
    }
    
    public static DailyboardPath Parse(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Dailyboard path cannot be empty", nameof(path));
        
        path = path.Trim();
        
        if (path.StartsWith('/') || path.EndsWith('/'))
            throw new ArgumentException("Dailyboard path cannot start or end with slash", nameof(path));
        
        var segments = path.Split('/');
        
        if (segments.Length == 0)
            throw new ArgumentException("Invalid dailyboard path", nameof(path));
        
        var lastSegment = segments[^1];
        
        // Is the last segment a date?
        if (YYMMDDDate.TryParse(lastSegment, out var date) && segments.Length > 1)
        {
            var categorySegments = segments.Take(segments.Length - 1);
            var categoryPath = CategoryPath.FromSegments(categorySegments.ToArray());
            
            return new DailyboardPath(categoryPath, date);
        }
        
        // Only category path, no date
        var onlyCategoryPath = CategoryPath.FromSegments(segments);
        return new DailyboardPath(onlyCategoryPath);
    }
    
    public static DailyboardPath Create(CategoryPath categoryPath, YYMMDDDate? date = null)
    {
        if (categoryPath is null)
            throw new ArgumentNullException(nameof(categoryPath));
        
        return new DailyboardPath(categoryPath, date);
    }
    
    public static DailyboardPath ForToday(CategoryPath categoryPath)
    {
        if (categoryPath is null)
            throw new ArgumentNullException(nameof(categoryPath));
        
        return new DailyboardPath(categoryPath, YYMMDDDate.Today());
    }
    
    public static bool TryParse(string path, out DailyboardPath? result)
    {
        result = null;
        
        try
        {
            result = Parse(path);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public bool HasDate => Date != null;
    
    public DailyboardPath? Parent
    {
        get
        {
            if (!HasDate)
            {
                var parentCategory = CategoryPath.Parent;
                return parentCategory != null 
                    ? new DailyboardPath(parentCategory, null) 
                    : null;
            }
            
            return new DailyboardPath(CategoryPath, null);
        }
    }
    
    public DailyboardPath? WithDate(YYMMDDDate date)
    {
        if (date is null)
            throw new ArgumentNullException(nameof(date));
        
        return new DailyboardPath(CategoryPath, date);
    }
    
    public DailyboardPath WithoutDate()
    {
        return new DailyboardPath(CategoryPath, null);
    }
    
    public bool IsChildOf(DailyboardPath parent)
    {
        if (parent is null) return false;
        if (parent == this) return false;
        
        if (parent.HasDate)
            return false;
        
        return CategoryPath.IsChildOf(parent.CategoryPath);
    }
    
    public bool IsChildOf(CategoryPath parentCategory)
    {
        if (parentCategory is null) return false;
        return CategoryPath.IsChildOf(parentCategory);
    }
    
    public string ToUrlString()
    {
        return HasDate 
            ? $"{CategoryPath}/{Date}"
            : CategoryPath.ToString();
    }
    
    public override string ToString() => ToUrlString();
    
    public string[] GetSegments()
    {
        var categorySegments = CategoryPath.Segments;
        
        if (!HasDate)
            return categorySegments;
        
        var result = new string[categorySegments.Length + 1];
        Array.Copy(categorySegments, result, categorySegments.Length);
        result[^1] = Date!.ToString();
        
        return result;
    }
    
    public IEnumerable<DailyboardPath> GetHierarchy()
    {
        foreach (var category in CategoryPath.GetHierarchy())
        {
            yield return new DailyboardPath(category, null);
        }
        
        if (HasDate)
        {
            yield return new DailyboardPath(CategoryPath, Date);
        }
    }
    
    public IEnumerable<DailyboardPath> GetDateHierarchy()
    {
        if (!HasDate)
        {
            yield return this;
            yield break;
        }
        
        yield return new DailyboardPath(CategoryPath, null);
        yield return this;
    }
    
    public DailyboardPath Combine(CategoryPath additionalPath)
    {
        ArgumentNullException.ThrowIfNull(additionalPath);

        var combinedSegments = GetSegments()
            .Concat(additionalPath.Segments)
            .ToArray();
        
        var combinedCategoryPath = CategoryPath.FromSegments(combinedSegments);
        
        return new DailyboardPath(combinedCategoryPath, Date);
    }
}