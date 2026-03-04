using HeuteApp.Core.ValueObjects;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.Aggregates.Layout;

public class LayoutSection
{
    protected LayoutSection() { }

    protected LayoutSection(LayoutSectionDefinition definition)
    {
        Id = Guid.NewGuid();
        Name = definition.Key.Name;
        Area = definition.Props.Area;
    }

    public static LayoutSection Create(LayoutSectionDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return new LayoutSection(definition);
    }

    //

    public Guid Id { get; private set; }

    public string Name { get; private set;} = null!;

    public GridRect Area { get; internal set; } = null!;
}