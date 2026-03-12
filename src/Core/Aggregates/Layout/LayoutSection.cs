using HeuteApp.Core.ValueObjects;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.Aggregates.Layout;

public class LayoutSection
{
    protected LayoutSection() { }

    protected LayoutSection(LayoutSectionDefinition definition)
    {
        Id = Guid.NewGuid();
        Name = definition.Name;
        Position = definition.Position;
    }

    public static LayoutSection Create(LayoutSectionDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return new LayoutSection(definition);
    }

    //

    public Guid Id { get; private set; } = Guid.Empty;

    public string Name { get; private set;} = string.Empty;

    public GridRect Position { get; internal set; } = GridRect.Empty;
}