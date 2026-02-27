using HeuteApp.Core.Aggregates;

namespace HeuteApp.Infrastructure.Models;

public class HeuteBoardModel : HeuteBoard
{
    private HeuteBoardModel() : base(Guid.Empty, Guid.Empty, Guid.Empty, default)
    {
        
    }

    public HeuteBoardModel(Guid id, Guid ownerId, Guid layoutId, DateOnly date) : base(id, ownerId, layoutId, date)
    {
        
    }

    public List<BoardCardModel> Cards { get; set; } = [];
}