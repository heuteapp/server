using HeuteApp.Core.Aggregates;
using HeuteApp.Core.Entities;

namespace HeuteApp.Core.Mappers;

public static partial class LayoutMapper
{
    public static LayoutSectionProps ToProps(this LayoutSection section)
    {
        ArgumentNullException.ThrowIfNull(section);

        return new LayoutSectionProps(
            section.Rect,
            section.Size
        );
    }

    //
    
    public static LayoutSection ToDomain(this LayoutSectionProps props, Guid id, string name)
    {
        ArgumentNullException.ThrowIfNull(props);

        return LayoutSection.Create(id, name, props);
    }
}