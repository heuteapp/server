using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.Mappers;

public static partial class LayoutMapper
{
    public static LayoutSectionDefinition ToProps(this LayoutSection section)
    {
        ArgumentNullException.ThrowIfNull(section);

        return new LayoutSectionDefinition(
            new LayoutSectionKey(section.Name),
            new LayoutSectionProps(section.Position)
        );
    }

    //

    public static LayoutSection ToDomain(this LayoutSectionDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        return LayoutSection.Create(definition);
    }
}