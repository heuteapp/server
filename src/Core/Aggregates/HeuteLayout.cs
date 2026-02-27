using HeuteApp.Core.Entities;
using HeuteApp.Core.Mappers;

namespace HeuteApp.Core.Aggregates;

public class HeuteLayout
{
    readonly Guid id;

    readonly Guid ownerId;

    readonly string name;

    readonly int version;

    private readonly Dictionary<Guid, LayoutSection> m_sectionDictionary = [];

    //

    public HeuteLayout(Guid id, Guid ownerId, string name, int version, HeuteLayoutProps props)
    {
        ArgumentNullException.ThrowIfNull(props);

        this.id = id;
        this.ownerId = ownerId;
        this.name = name;
        this.version = version;

        foreach (var section in props.Sections)
        {
            DoAddSection(section.Id, section.Name, section.ToProps());
        }
    }

    //

    public Guid Id => id;

    public Guid OwnerId => ownerId;

    public string Name => name;

    public int Version => version;

    public IReadOnlyCollection<LayoutSection> Sections => m_sectionDictionary.Values;

    //

    public bool HasSection(Guid sectionId)
    {
        return m_sectionDictionary.ContainsKey(sectionId);
    }

    //

    private void DoAddSection(Guid sectionId, string name, LayoutSectionProps props)
    {
        var section = LayoutSection.Create(sectionId, name, props);
        m_sectionDictionary.Add(sectionId, section);
    }
}

public sealed record HeuteLayoutProps(
    IEnumerable<LayoutSection> Sections
);