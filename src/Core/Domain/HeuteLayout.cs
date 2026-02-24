namespace HeuteApp.Core.Domain;

public class HeuteLayout(string id)
{

    private HeuteLayoutSection[]? m_sections = null;

    private readonly Dictionary<string, HeuteLayoutSection> m_sectionDictionary = [];

    //

    public string Id => id;

    public HeuteLayoutSection[] Sections => m_sections ??= [.. m_sectionDictionary.Values];
}