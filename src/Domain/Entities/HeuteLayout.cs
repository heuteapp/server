namespace HeuteApp.Domain.Entities;

public class HeuteLayout(Guid id, Guid ownerId, string name, int version)
{
    private HeuteLayoutSection[]? m_sections = null;

    private readonly Dictionary<Guid, HeuteLayoutSection> m_sectionDictionary = [];

    //

    public Guid Id => id;

    public Guid OwnerId => ownerId;

    public string Name => name;

    public int Version => version;

    public HeuteLayoutSection[] Sections => m_sections ??= [.. m_sectionDictionary.Values];

    //

    public bool HasSection(Guid sectionId)
    {
        return m_sectionDictionary.ContainsKey(sectionId);
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
        return new HeuteLayout(snapshot.Id, snapshot.OwnerId, snapshot.Name, snapshot.Version);
    }

    public static HeuteLayout FromProps(Guid id, Guid ownerId, string name, int version, HeuteLayoutProps props)
    {
        ArgumentNullException.ThrowIfNull(props);
        return new HeuteLayout(id, ownerId, name, version);
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
    IEnumerable<HeuteLayoutSectionSnapshot> Sections
);