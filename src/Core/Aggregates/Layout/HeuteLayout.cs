using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.Aggregates.Layout;

public class HeuteLayout
{
    private readonly List<LayoutSection> m_sections = [];

    private HeuteLayout() { }

    protected HeuteLayout(LayoutDefinition definition)
    {
        Id = Guid.NewGuid();
        OwnerId = definition.OwnerId;
        Name = definition.Key.Name;
        Version = definition.Key.Version;

        foreach (var sectionDef in definition.Props.Sections)
        {
            var section = Internal_CreateSection(sectionDef);
            m_sections.Add(section);
        }
    }

    public static HeuteLayout Create(LayoutDefinition definition)
    {
        return new HeuteLayout(definition);
    }

    //

    public Guid Id { get; private set; }

    public Guid OwnerId { get; private set; }

    public string Name { get; private set; } = null!;

    public int Version {get; private set; }

    public IReadOnlyCollection<LayoutSection> Sections => m_sections;

    //

    internal protected virtual LayoutSection Internal_CreateSection(LayoutSectionDefinition definition)
    {
        return LayoutSection.Create(definition);
    }
}