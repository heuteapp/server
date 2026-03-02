using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.Aggregates.Layout;

public class HeuteLayout
{
    private readonly List<LayoutSection> m_sections = [];

    protected virtual LayoutSection OnCreateSection(Guid id, string name, LayoutSectionProps props)
    {
        return LayoutSection.Create(id, name, props);
    }

    protected HeuteLayout()
    {
        Key = null!;
    }

    protected HeuteLayout(Guid id, LayoutKey key)
    {
        Id = id;
        Key = key;
    }

    public static HeuteLayout Create(Guid id, Guid ownerId, string name, int version)
    {
        return new HeuteLayout(id, new LayoutKey(ownerId, name, version));
    }

    //

    public Guid Id { get; private set; }

    public LayoutKey Key { get; private set; }

    public Guid? OwnerId => Key.OwnerId;

    public string Name => Key.Name;

    public int? Version => Key.Version;

    public IReadOnlyCollection<LayoutSection> Sections => m_sections;

    //

    public void AddSection(Guid sectionId, string name, LayoutSectionProps props)
    {
        ArgumentNullException.ThrowIfNull(props);

        if (HasSection(sectionId))
        {
            throw new InvalidOperationException($"Section with id {sectionId} already exists.");
        }

        if(m_sections.Count >= 4)
        {
            throw new InvalidOperationException("Layout already has maximum number of sections (4).");
        }

        DoAddSection(sectionId, name, props);
    }

    public bool HasSection(Guid sectionId)
    {
        return m_sections.Any(s => s.Id == sectionId);
    }

    //

    private void DoAddSection(Guid sectionId, string name, LayoutSectionProps props)
    {
        var section = OnCreateSection(sectionId, name, props);
        m_sections.Add(section);
    }
}