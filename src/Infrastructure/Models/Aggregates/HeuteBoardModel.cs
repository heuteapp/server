using HeuteApp.Core.Aggregates;
using HeuteApp.Core.Entities;
using HeuteApp.Infrastructure.Models.Entities;

namespace HeuteApp.Infrastructure.Models.Aggregates;

public class HeuteBoardModel : HeuteBoard
{
    protected override BoardCard CardInstance(Guid id, BoardCardProps props)
    {
        return new BoardCardModel(id, boardId: Id, props);
    }

    private HeuteBoardModel() : base(Guid.Empty, Guid.Empty, Guid.Empty, default)
    {
        
    }

    public HeuteBoardModel(Guid id, Guid ownerId, Guid layoutId, DateOnly date) : base(id, ownerId, layoutId, date)
    {
        
    }

    public ICollection<BoardCard> Cards => m_cards;
}