using HeuteApp.Core.Entities;
 
namespace HeuteApp.Core.Aggregates;

public class HeuteLayout(Guid id, Guid ownerId, string name, int version)
{
    private readonly Dictionary<Guid, LayoutSection> m_sectionDictionary = [];

    protected HeuteLayout() : this(Guid.Empty, Guid.Empty, string.Empty, 0)
    {
        
    }

    //

    public Guid Id { get; private set; } = id;

    public Guid OwnerId { get; private set; } = ownerId;

    public string Name { get; private set; } = name;

    public int Version { get; private set; } = version;

    public IReadOnlyCollection<LayoutSection> Sections => m_sectionDictionary.Values;

    //

    public void AddSection(Guid sectionId, string name, LayoutSectionProps props)
    {
        ArgumentNullException.ThrowIfNull(props);

        if (HasSection(sectionId))
        {
            throw new InvalidOperationException($"Section with id {sectionId} already exists.");
        }

        if(m_sectionDictionary.Count >= 4)
        {
            throw new InvalidOperationException("Layout already has maximum number of sections (4).");
        }

        DoAddSection(sectionId, name, props);
    }

    public bool HasSection(Guid sectionId)
    {
        return m_sectionDictionary.ContainsKey(sectionId);
    }

    //

    private void DoAddSection(Guid sectionId, string name, LayoutSectionProps props)
    {
        var section = new LayoutSection(sectionId, name, props);
        m_sectionDictionary.Add(sectionId, section);
    }
}