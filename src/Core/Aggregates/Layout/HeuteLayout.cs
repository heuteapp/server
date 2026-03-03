using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.Aggregates.Layout;

public class HeuteLayout
{
    private readonly LayoutKey m_key = null!;

    private readonly List<LayoutSection> m_sections = [];

    protected HeuteLayout()
    {
        m_key = null!;
    }

    protected HeuteLayout(LayoutDefinition definition)
    {
        Id = Guid.NewGuid();
        OwnerId = definition.OwnerId;
        m_key = definition.Key;

        foreach (var sectionDef in definition.Props.Sections)
        {
            var section = Internal_CreateSection(sectionDef);
            Internal_AddSection(section);
        }
    }

    public static HeuteLayout Create(LayoutDefinition definition)
    {
        return new HeuteLayout(definition);
    }

    //

    public Guid Id { get; private set; }

    public Guid OwnerId { get; private set; }

    public string Name => m_key.Name;

    public int Version => m_key.Version;

    public IReadOnlyCollection<LayoutSection> Sections => m_sections;

    //

    public void AddSection(LayoutSectionDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        if (m_sections.Count >= 4)
        {
            throw new InvalidOperationException("Layout already has maximum number of sections (4).");
        }

        var section = Internal_CreateSection(definition);
        Internal_AddSection(section);
    }

    public bool HasSection(Guid sectionId)
    {
        return m_sections.Any(s => s.Id == sectionId);
    }

    //

    internal protected virtual LayoutSection Internal_CreateSection(LayoutSectionDefinition definition)
    {
        return LayoutSection.Create(definition);
    }

    internal protected virtual void Internal_AddSection(LayoutSection section)
    {
        if(m_sections.Any(s => s.Id == section.Id))
        {
            throw new InvalidOperationException($"Section with id {section.Id} already exists.");
        }

        m_sections.Add(section);
    }
}