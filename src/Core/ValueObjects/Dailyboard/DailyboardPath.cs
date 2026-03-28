using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Core.ValueObjects.Dailyboard;

public sealed record DailyboardPath
{
    public CategoryPath CategoryPath { get; }
    public YYMMDDDate? Date { get; }
    
    private DailyboardPath(CategoryPath categoryPath, YYMMDDDate? date = null)
    {
        CategoryPath = categoryPath;
        Date = date;
    }
    
    public static DailyboardPath Parse(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Dailyboard path cannot be empty", nameof(path));
        
        var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        
        if (segments.Length == 0)
            throw new ArgumentException("Invalid dailyboard path", nameof(path));
        
        var lastSegment = segments[^1];
        
        // Son segment tarih mi?
        if (IsDateSegment(lastSegment) && segments.Length > 1)
        {
            var categorySegments = segments.Take(segments.Length - 1);
            var categoryPath = CategoryPath.FromSegments(categorySegments.ToArray());
            var date = YYMMDDDate.FromString(lastSegment);
            
            return new DailyboardPath(categoryPath, date);
        }
        
        // Sadece category path
        var onlyCategoryPath = CategoryPath.FromSegments(segments);
        return new DailyboardPath(onlyCategoryPath);
    }
    
    public static DailyboardPath Create(CategoryPath categoryPath, YYMMDDDate? date = null)
    {
        return new DailyboardPath(categoryPath, date);
    }
    
    public static DailyboardPath ForToday(CategoryPath categoryPath)
    {
        return new DailyboardPath(categoryPath, YYMMDDDate.Today());
    }
    
    private static bool IsDateSegment(string segment)
    {
        return segment.Length == 6 && segment.All(char.IsDigit);
    }
    
    public bool HasDate => Date != null;
    
    public string ToUrlString()
    {
        return HasDate 
            ? $"{CategoryPath}/{Date}"
            : CategoryPath.ToString();
    }
    
    public override string ToString() => ToUrlString();
}