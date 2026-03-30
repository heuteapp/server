using System.Text.Json.Serialization;

namespace HeuteApp.Core.ValueObjects.Dailyboard;

public sealed record DailyboardCardProps
{
    public static DailyboardCardProps Empty => new();

    //

    public string? Title { get; private set; } = null!;

    public string? SectionName { get; private set; } = null;

    public int? ColIndex { get; private set; } = null;

    public int? RowIndex { get; private set; } = null;

    public int? ColSpan { get; private set; } = null;

    public int? RowSpan { get; private set; } = null;

    //

    public DailyboardCardProps() { }

    public DailyboardCardProps(string title, string sectionName, int colIndex, int rowIndex, int colSpan, int rowSpan)
    {
        Title = title;
        SectionName = sectionName;
        ColIndex = colIndex;
        RowIndex = rowIndex;
        ColSpan = colSpan;
        RowSpan = rowSpan;
    }

    public DailyboardCardProps(DailyboardCardContent content, DailyboardCardPlacement? placement)
    {
        Title = content.Title;
        SectionName = placement?.SectionName;
        ColIndex = placement?.ColIndex;
        RowIndex = placement?.RowIndex;
        ColSpan = placement?.ColSpan;
        RowSpan = placement?.RowSpan;
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