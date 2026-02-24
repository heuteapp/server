using HeuteApp.Core.Domain.ValueObjects;

namespace HeuteApp.Core.Domain;

public class HeuteBoardCard(Guid id)
{
    private string? m_sectionId = null;

    private GridRect? m_position = null;

    private string? m_title = null;

    //

    public Guid Id => id;

    public string? SectionId => m_sectionId;

    public GridRect? Position => m_position;

    public string? Title => m_title;

    //

    public bool IsPlaced
    {
        get
        {
            return m_sectionId != null && m_position != null;
        }
    }

    //

    internal void DoPlace(string sectionId, GridRect position)
    {
        ArgumentNullException.ThrowIfNull(sectionId);
        ArgumentNullException.ThrowIfNull(position);

        m_sectionId = sectionId;
        m_position = position;
    }

    internal void DoUnplace()
    {
        m_sectionId = null;
        m_position = null;
    }

    internal void DoSetTitle(string title)
    {
        ArgumentNullException.ThrowIfNull(title);

        m_title = title;
    }
}