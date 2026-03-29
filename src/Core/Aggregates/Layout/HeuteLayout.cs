using HeuteApp.Core.ValueObjects;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.Aggregates.Layout;

public class HeuteLayout
{
    private readonly List<LayoutSection> m_sections = [];

    protected HeuteLayout() { }

    protected HeuteLayout(Guid userId, LayoutDefinition definition)
    {
        Id = Guid.NewGuid();
        OwnerId = userId;
        Name = definition.Key.Name;
        Version = definition.Key.Version;
        Dimensions = definition.Props.Dimensions;

        foreach (var sectionDef in definition.Props.Sections)
        {
            var section = Internal_CreateSection(sectionDef);

            // check for overlapping sections
            if(m_sections.Any(s => s.Position.Overlaps(section.Position)))
            {
                throw new InvalidOperationException($"Section {section.Name} overlaps with another section.");
            }

            m_sections.Add(section);
        }
    }

    public static HeuteLayout Create(Guid userId, LayoutDefinition definition)
    {
        return new HeuteLayout(userId, definition);
    }

    //

    public Guid Id { get; private set; } = Guid.Empty;

    public Guid OwnerId { get; private set; } = Guid.Empty;

    public string Name { get; private set; } = string.Empty;

    public int Version { get; private set; } = 0;

    public GridDimensions Dimensions { get; private set; } = GridDimensions.Empty;

    public IReadOnlyCollection<LayoutSection> Sections => m_sections;

    //

    internal protected virtual LayoutSection Internal_CreateSection(LayoutSectionDefinition definition)
    {
        return LayoutSection.Create(definition);
    }
}