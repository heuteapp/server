using System.Text.Json.Serialization;

namespace HeuteApp.Core.ValueObjects.Dailyboard;

public record DailyboardCardDefinition
{
    public static DailyboardCardDefinition Empty => new();

    //

    public string Name { get; private set; } = null!;

    public string? Title { get; private set; } = null!;

    public string? SectionName { get; private set; } = null;

    public int? ColIndex { get; private set; } = null;

    public int? RowIndex { get; private set; } = null;

    public int? ColSpan { get; private set; } = null;

    public int? RowSpan { get; private set; } = null;

    //

    public DailyboardCardDefinition() { }

    public DailyboardCardDefinition(string name, string? title, string? sectionName, int? colIndex, int? rowIndex, int? colSpan, int? rowSpan)
    {
        Name = name;
        Title = title;
        SectionName = sectionName;
        ColIndex = colIndex;
        RowIndex = rowIndex;
        ColSpan = colSpan;
        RowSpan = rowSpan;
    }
    
    public DailyboardCardDefinition(DailyboardCardKey key, DailyboardCardProps props)
    {
        Name = key.Name;
        Title = props.Title;
        SectionName = props.SectionName;
        ColIndex = props.ColIndex;
        RowIndex = props.RowIndex;
        ColSpan = props.ColSpan;
        RowSpan = props.RowSpan;
    }

    //

    [JsonIgnore]
    public DailyboardCardKey Key => new(Name);

    [JsonIgnore]
    public DailyboardCardProps Props => new(Content, Placement);
    
    [JsonIgnore]
    public DailyboardCardContent Content => new(Title);
    
    [JsonIgnore]
    public DailyboardCardPlacement? Placement => 
        SectionName is not null && ColIndex is not null && RowIndex is not null && ColSpan is not null && RowSpan is not null
        ? new DailyboardCardPlacement(SectionName, ColIndex.Value, RowIndex.Value, ColSpan.Value, RowSpan.Value)
        : null;
}