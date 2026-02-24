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

        m_sectionDictionary.Add(section.Id, section);
        m_sections = null;
    }

    public void AddSections(IEnumerable<HeuteLayoutSection> sections)
    {
        ArgumentNullException.ThrowIfNull(sections);

        foreach (var section in sections)
        {
            AddSection(section);
        }
    }
}