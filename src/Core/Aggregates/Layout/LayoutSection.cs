using HeuteApp.Core.ValueObjects;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.Aggregates.Layout;

public class LayoutSection
{
    protected LayoutSection()
    {
        Id = Guid.Empty;
        Name = string.Empty;
        Rect = null!;
        Size = null!;
    }

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

    public string Name { get; private set;}

    public Rect Rect { get; internal set; }

    public GridSize Size { get; internal set; }
}