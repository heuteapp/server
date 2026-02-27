using HeuteApp.Core.Aggregates;
using HeuteApp.Core.Entities;

namespace HeuteApp.Core.Mappers;

public static partial class LayoutMapper
{
    public static HeuteLayoutProps ToProps(this HeuteLayout layout)
    {
        ArgumentNullException.ThrowIfNull(layout);

        return new HeuteLayoutProps(
            [.. layout.Sections]
        );
    }

    public static LayoutSectionProps ToProps(this LayoutSection section)
    {
        ArgumentNullException.ThrowIfNull(section);

        return new LayoutSectionProps(
            section.Rect,
            section.Size
        );
    }

    //

    public static HeuteLayout ToDomain(this HeuteLayoutProps props, Guid id, Guid ownerId, string name, int version)
    {
        ArgumentNullException.ThrowIfNull(props);

        return new HeuteLayout(id, ownerId, name, version, props);
    }

    public static LayoutSection ToDomain(this LayoutSectionProps props, Guid id, string name)
    {
        ArgumentNullException.ThrowIfNull(props);

        return LayoutSection.Create(id, name, props);
    }
}