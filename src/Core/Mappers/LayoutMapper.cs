using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects.Layout;

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

    public static LayoutSection ToDomain(this LayoutSectionProps props, Guid id, LayoutSectionKey key)
    {
        ArgumentNullException.ThrowIfNull(props);

        return LayoutSection.Create(id, key, props);
    }
}