using System.Text.Json.Serialization;

namespace HeuteApp.Core.ValueObjects.Dailyboard;

public sealed record DailyboardCardProps(
    string? Title,
    string? SectionName,
    int? ColIndex,
    int? RowIndex,
    int? ColSpan,
    int? RowSpan)
{
    public static DailyboardCardProps Empty => new();

    //

    public DailyboardCardProps() 
        : this(title:null, null) { }

    public DailyboardCardProps(DailyboardCardContent content, DailyboardCardPlacement? placement)
        : this(
            content.Title,
            placement?.SectionName,
            placement?.ColIndex,
            placement?.RowIndex,
            placement?.ColSpan,
            placement?.RowSpan)
    {
    }

    public DailyboardCardProps(string? title, DailyboardCardPlacement? placement)
        : this(title, placement?.SectionName, placement?.ColIndex, placement?.RowIndex, placement?.ColSpan, placement?.RowSpan)
    {
    }

    public DailyboardCardProps(DailyboardCardContent content, string? sectionName, int? colIndex, int? rowIndex, int? colSpan, int? rowSpan)
        : this(content.Title, sectionName, colIndex, rowIndex, colSpan, rowSpan)
    {
    }

    //

    [JsonIgnore]
    public DailyboardCardContent Content => new(Title);
    
    [JsonIgnore]
    public DailyboardCardPlacement? Placement => 
        SectionName is not null && ColIndex is not null && RowIndex is not null && ColSpan is not null && RowSpan is not null
        ? new DailyboardCardPlacement(SectionName, ColIndex.Value, RowIndex.Value, ColSpan.Value, RowSpan.Value)
        : null;
}