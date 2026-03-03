using HeuteApp.Core.ValueObjects;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.Aggregates.Layout;

public class LayoutSection
{
    private readonly LayoutSectionKey m_key = null!;

    protected LayoutSection() { }

    protected LayoutSection(Guid id, LayoutSectionKey key, LayoutSectionProps props)
    {
        Id = id;
        m_key = key;
        Rect = props.Rect;
        Size = props.Size;
    }

    public static LayoutSection Create(Guid id, LayoutSectionKey key, LayoutSectionProps props)
    {
        ArgumentNullException.ThrowIfNull(props);
        return new LayoutSection(id, key, props);
    }

    //

    public Guid Id { get; private set; }

    public string Name => m_key.Name;

    public Rect Rect { get; internal set; } = null!;

    public GridSize Size { get; internal set; } = null!;
}

public sealed record LayoutSectionProps(
    Rect Rect,
    GridSize Size
);