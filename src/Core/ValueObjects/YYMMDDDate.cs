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
        
        if (!IsValidFormat(dateStr))
            throw new ArgumentException($"Invalid date format: {dateStr}. Expected yyMMdd");
        
        var yy = int.Parse(dateStr.AsSpan(0, 2));
        var mm = int.Parse(dateStr.AsSpan(2, 2));
        var dd = int.Parse(dateStr.AsSpan(4, 2));
        
        var date = new DateTime(2000 + yy, mm, dd);
        return new YYMMDDDate(date);
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
}