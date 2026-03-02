 namespace HeuteApp.Core.Aggregates.Layout;

public class Layout
{
    private readonly List<LayoutSection> m_sections = [];

    protected virtual LayoutSection OnCreateSection(Guid id, string name, LayoutSectionProps props)
    {
        return LayoutSection.Create(id, name, props);
    }

    protected Layout()
    {
        
    }

    protected Layout(Guid id, Guid ownerId, string name, int version)
    {
        Id = id;
        OwnerId = ownerId;
        Name = name;
        Version = version;
    }

    public static Layout Create(Guid id, Guid ownerId, string name, int version)
    {
        return new Layout(id, ownerId, name, version);
    }

    //

    public Guid Id { get; private set; }

    public Guid OwnerId { get; private set; }

    public string Name { get; private set; } = null!;

    public int Version { get; private set; } = 0;

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