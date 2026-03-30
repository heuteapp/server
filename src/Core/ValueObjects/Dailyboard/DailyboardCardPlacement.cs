using System.Text.Json.Serialization;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.ValueObjects.Dailyboard;

public sealed record DailyboardCardPlacement(
    string SectionName,
    int ColIndex,
    int RowIndex,
    int ColSpan,
    int RowSpan)
{
    public static DailyboardCardPlacement Empty => new();

    //

    public DailyboardCardPlacement() 
        : this(string.Empty, -1, -1, 0, 0) { }

    public DailyboardCardPlacement(LayoutSectionKey section, int colIndex, int rowIndex, int colSpan, int rowSpan)
        : this(section.Name, colIndex, rowIndex, colSpan, rowSpan)
    {
    }

    public DailyboardCardPlacement(string sectionName, GridRect position)
        : this(sectionName, position.ColIndex, position.RowIndex, position.ColSpan, position.RowSpan)
    {
    }

    public DailyboardCardPlacement(LayoutSectionKey section, GridRect position)
        : this(section.Name, position.ColIndex, position.RowIndex, position.ColSpan, position.RowSpan)
    {
    }

    //

    [JsonIgnore]
    public LayoutSectionKey Section => new(SectionName);

    [JsonIgnore]
    public GridRect Position => new(ColIndex, RowIndex, ColSpan, RowSpan);
}