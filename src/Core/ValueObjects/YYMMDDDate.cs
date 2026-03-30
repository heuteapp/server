namespace HeuteApp.Core.ValueObjects;

public sealed record YYMMDDDate
{
    public DateTime Value { get; }
    
    private YYMMDDDate(DateTime value)
    {
        Value = value;
    }
    
    public static YYMMDDDate FromString(string dateStr)
    {
        if (string.IsNullOrWhiteSpace(dateStr))
            throw new ArgumentException("Date cannot be empty", nameof(dateStr));
        
        if (!TryParse(dateStr, out var date))
            throw new ArgumentException($"Invalid date format: {dateStr}. Expected yyMMdd (e.g., 260409 for 2026-04-09)", nameof(dateStr));
        
        return date!;
    }
    
    public static bool TryParse(string dateStr, out YYMMDDDate? date)
    {
        date = null;
        
        if (string.IsNullOrWhiteSpace(dateStr))
            return false;
        
        if (!IsValidFormat(dateStr))
            return false;
        
        var yy = int.Parse(dateStr.AsSpan(0, 2));
        var mm = int.Parse(dateStr.AsSpan(2, 2));
        var dd = int.Parse(dateStr.AsSpan(4, 2));
        
        // Validate month and day ranges
        if (mm < 1 || mm > 12)
            return false;
        
        if (dd < 1 || dd > DateTime.DaysInMonth(2000 + yy, mm))
            return false;
        
        try
        {
            var dateTime = new DateTime(2000 + yy, mm, dd);
            date = new YYMMDDDate(dateTime);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public static YYMMDDDate FromDateTime(DateTime date)
    {
        return new YYMMDDDate(date);
    }
    
    public static YYMMDDDate Today()
    {
        return new YYMMDDDate(DateTime.Today);
    }
    
    private static bool IsValidFormat(string dateStr)
    {
        return dateStr.Length == 6 && dateStr.All(char.IsDigit);
    }
    
    public string ToShortString()
    {
        return Value.ToString("yyMMdd");
    }
    
    public string ToIsoString()
    {
        return Value.ToString("yyyy-MM-dd");
    }
    
    public override string ToString() => ToShortString();

    public DateTime ToDateTime()
    {
        return Value;
    }
    
    public DateOnly ToDateOnly()
    {
        return DateOnly.FromDateTime(Value);
    }
}