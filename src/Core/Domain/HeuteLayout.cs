namespace HeuteApp.Core.Domain;

public class HeuteLayout(Guid id, string name)
{
    private HeuteLayoutSection[]? m_sections = null;

    private readonly Dictionary<Guid, HeuteLayoutSection> m_sectionDictionary = [];

    //

    public Guid Id => id;

    public string Name => name;

    public HeuteLayoutSection[] Sections => m_sections ??= [.. m_sectionDictionary.Values];

    //

    public void AddSection(HeuteLayoutSection section)
    {
        ArgumentNullException.ThrowIfNull(section);
        DoAddSection(section);
    }

    public void TryAddSection(HeuteLayoutSection section)
    {
        ArgumentNullException.ThrowIfNull(section);

        if (HasSection(section.Id))
        {
            return;
        }

        DoAddSection(section);
    }

    public void AddSections(IEnumerable<HeuteLayoutSection> sections)
    {
        ArgumentNullException.ThrowIfNull(sections);

        foreach (var section in sections)
        {
            TryAddSection(section);
        }
    }

    public bool HasSection(Guid sectionId)
    {
        return m_sectionDictionary.ContainsKey(sectionId);
    }

    //

    private void DoAddSection(HeuteLayoutSection section)
    {
        m_sectionDictionary.Add(section.Id, section);
        m_sections = null;
    }

    //

    public HeuteLayoutSnapshot ToSnapshot()
    {
        return new HeuteLayoutSnapshot(
            Id,
            Name,
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

        var layout = new HeuteLayout(snapshot.Id, snapshot.Name);
        layout.AddSections(snapshot.Props.Sections.Select(HeuteLayoutSection.FromSnapshot));

        return layout;
    }

    public static HeuteLayout FromProps(Guid id, string name, HeuteLayoutProps props)
    {
        ArgumentNullException.ThrowIfNull(props);

        var layout = new HeuteLayout(id, name);
        layout.AddSections(props.Sections.Select(HeuteLayoutSection.FromSnapshot));

        return layout;
    }
}

public sealed record HeuteLayoutSnapshot(
    Guid Id,
    string Name,
    HeuteLayoutProps Props
);

public sealed record HeuteLayoutProps(
    IEnumerable<HeuteLayoutSectionSnapshot> Sections
);