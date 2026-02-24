using HeuteApp.Core.Domain.ValueObjects;

namespace HeuteApp.Core.Domain;

public class HeuteBoardCard(Guid id)
{
    private string? m_sectionId = null;
    private GridRect? m_position = null;

    //

    public Guid Id => id;

    public string? SectionId => m_sectionId;

    public GridRect? Position => m_position;

    //

    internal void DoPlace(string sectionId, GridRect position)
    {
        ArgumentNullException.ThrowIfNull(sectionId);
        ArgumentNullException.ThrowIfNull(position);

        m_sectionId = sectionId;
        m_position = position;
    }
}