using HeuteApp.Core.Domain.ValueObjects;

namespace HeuteApp.Core.Domain;

public class HeuteBoardCard(Guid id)
{
    private GridRect? m_position = null;

    //

    public Guid Id => id;

    public GridRect? Position => m_position;
}