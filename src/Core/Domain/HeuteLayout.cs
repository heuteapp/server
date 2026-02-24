namespace HeuteApp.Core.Domain;

public class HeuteLayout(Guid id, string name)
{

    private HeuteLayoutSection[]? m_sections = null;

    private readonly Dictionary<string, HeuteLayoutSection> m_sectionDictionary = [];

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

    public bool HasSection(string sectionId)
    {
        ArgumentNullException.ThrowIfNull(sectionId);
        return m_sectionDictionary.ContainsKey(sectionId);
    }

    //

    private void DoAddSection(HeuteLayoutSection section)
    {
        m_sectionDictionary.Add(section.Id, section);
        m_sections = null;
    }
}