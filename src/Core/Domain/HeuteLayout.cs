namespace HeuteApp.Core.Domain;

public class HeuteLayout(string id)
{

    private HeuteLayoutSection[]? m_sections = null;

    private readonly Dictionary<string, HeuteLayoutSection> m_sectionDictionary = [];

    //

    public string Id => id;

    public HeuteLayoutSection[] Sections => m_sections ??= [.. m_sectionDictionary.Values];

    //

    public void AddSection(HeuteLayoutSection section)
    {
        ArgumentNullException.ThrowIfNull(section);
        DoAddSection(section);
    }

    public void AddSections(IEnumerable<HeuteLayoutSection> sections)
    {
        ArgumentNullException.ThrowIfNull(sections);

        foreach (var section in sections)
        {
            DoAddSection(section);
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