using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.Aggregates.Layout;

public class HeuteLayout
{
    private readonly LayoutKey m_key = null!;

    private readonly List<LayoutSection> m_sections = [];

    protected HeuteLayout()
    {
        m_key = null!;
    }

    protected HeuteLayout(Guid id, Guid ownerId, LayoutKey key)
    {
        Id = id;
        OwnerId = ownerId;
        m_key = key;
    }

    public static HeuteLayout Create(Guid id, Guid ownerId, LayoutKey key)
    {
        return new HeuteLayout(id, ownerId, key);
    }

    //

    public Guid Id { get; private set; }

    public Guid OwnerId { get; private set; }

    public string Name => m_key.Name;

    public int Version => m_key.Version;

    public IReadOnlyCollection<LayoutSection> Sections => m_sections;

    //

    public void AddSection(Guid sectionId, LayoutSectionKey key, LayoutSectionProps props)
    {
        ArgumentNullException.ThrowIfNull(props);

        if (HasSection(sectionId))
        {
            throw new InvalidOperationException($"Section with id {sectionId} already exists.");
        }

        if (m_sections.Count >= 4)
        {
            throw new InvalidOperationException("Layout already has maximum number of sections (4).");
        }

        var section = Internal_CreateSection(sectionId, key, props);
        Internal_AddSection(section);
    }

    public bool HasSection(Guid sectionId)
    {
        return m_sections.Any(s => s.Id == sectionId);
    }

    //

    internal protected virtual LayoutSection Internal_CreateSection(Guid id, LayoutSectionKey key, LayoutSectionProps props)
    {
        return LayoutSection.Create(id, key, props);
    }

    internal protected virtual void Internal_AddSection(LayoutSection section)
    {
        if(m_sections.Any(s => s.Id == section.Id))
        {
            throw new InvalidOperationException($"Section with id {section.Id} already exists.");
        }

        m_sections.Add(section);
    }
}