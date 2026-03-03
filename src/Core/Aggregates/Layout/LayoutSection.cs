using HeuteApp.Core.ValueObjects;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.Aggregates.Layout;

public class LayoutSection
{
    private LayoutSection() { }

    protected LayoutSection(LayoutSectionDefinition definition)
    {
        Id = Guid.NewGuid();
        Name = definition.Key.Name;
        Rect = definition.Props.Rect;
        Size = definition.Props.Size;
    }

    public static LayoutSection Create(LayoutSectionDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return new LayoutSection(definition);
    }

    //

    public Guid Id { get; private set; }

    public string Name { get; private set;} = null!;

    public Rect Rect { get; internal set; } = null!;

    public GridSize Size { get; internal set; } = null!;
}