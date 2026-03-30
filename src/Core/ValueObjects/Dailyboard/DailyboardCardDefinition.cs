using System.Text.Json.Serialization;

namespace HeuteApp.Core.ValueObjects.Dailyboard;

public sealed record DailyboardCardDefinition(
    string Name,
    string? Title,
    string? SectionName,
    int? ColIndex,
    int? RowIndex,
    int? ColSpan,
    int? RowSpan)
{
    public static DailyboardCardDefinition Empty => new("", null, null, null, null, null, null);

    //

    public DailyboardCardDefinition(DailyboardCardKey key, string? title, string? sectionName, int? colIndex, int? rowIndex, int? colSpan, int? rowSpan)
        : this(key.Name, title, sectionName, colIndex, rowIndex, colSpan, rowSpan)
    {
    }

    public DailyboardCardDefinition(string name, DailyboardCardProps props)
        : this(name, props.Title, props.SectionName, props.ColIndex, props.RowIndex, props.ColSpan, props.RowSpan)
    {
    }

    public DailyboardCardDefinition(DailyboardCardKey key, DailyboardCardProps props)
        : this(key.Name, props.Title, props.SectionName, props.ColIndex, props.RowIndex, props.ColSpan, props.RowSpan)
    {
    }

    public DailyboardCardDefinition(string name, DailyboardCardContent content, DailyboardCardPlacement? placement)
        : this(name, content.Title, placement?.SectionName, placement?.ColIndex, placement?.RowIndex, placement?.ColSpan, placement?.RowSpan)
    {
    }

    public DailyboardCardDefinition(DailyboardCardKey key, DailyboardCardContent content, DailyboardCardPlacement? placement)
        : this(key.Name, content.Title, placement?.SectionName, placement?.ColIndex, placement?.RowIndex, placement?.ColSpan, placement?.RowSpan)
    {
    }

    //

    public DailyboardCardKey Key => new(Name);

    public DailyboardCardProps Props => new(Content, Placement);

    public DailyboardCardContent Content => new(Title);

    public DailyboardCardPlacement? Placement => 
        SectionName is not null && ColIndex is not null && RowIndex is not null && ColSpan is not null && RowSpan is not null
        ? new DailyboardCardPlacement(SectionName, ColIndex.Value, RowIndex.Value, ColSpan.Value, RowSpan.Value)
        : null;
}