using HeuteApp.Core.Entities;

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
            DoAddSection(section.Id, section.Name, section.Props);
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
        var section = new LayoutSection(sectionId, name, props);
        m_sectionDictionary.Add(sectionId, section);
    }

    //

    public HeuteLayoutSnapshot ToSnapshot()
    {
        return new HeuteLayoutSnapshot(
            Id,
            OwnerId,
            Name,
            Version,
            new HeuteLayoutProps(
                Sections.Select(s => s.ToSnapshot())
            )
        );
    }

    public HeuteLayoutProps ToProps()
    {
        return new HeuteLayoutProps(
            Sections.Select(s => s.ToSnapshot())
        );
    }

    public static HeuteLayout FromSnapshot(HeuteLayoutSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        return new HeuteLayout(snapshot.Id, snapshot.OwnerId, snapshot.Name, snapshot.Version, snapshot.Props);
    }

    public static HeuteLayout FromProps(Guid id, Guid ownerId, string name, int version, HeuteLayoutProps props)
    {
        ArgumentNullException.ThrowIfNull(props);
        return new HeuteLayout(id, ownerId, name, version, props);
    }
}

public sealed record HeuteLayoutSnapshot(
    Guid Id,
    Guid OwnerId,
    string Name,
    int Version,
    HeuteLayoutProps Props
);

public sealed record HeuteLayoutProps(
    IEnumerable<LayoutSectionSnapshot> Sections
);