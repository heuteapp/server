using HeuteApp.Core.Aggregates;
using HeuteApp.Core.Entities;
using HeuteApp.Infrastructure.Models.Entities;

namespace HeuteApp.Infrastructure.Models.Aggregates;

public class HeuteBoardModel : HeuteBoard
{    
    protected override BoardCard OnCreateCard(Guid id, BoardCardProps props)
    {
        return BoardCardModel.Create(id, this, props);
    }

    protected HeuteBoardModel() { }

    protected HeuteBoardModel(Guid id, Guid ownerId, Guid layoutId, DateOnly date) : base(id, ownerId, layoutId, date) { }

    //

    public static new HeuteBoardModel Create(Guid id, Guid ownerId, Guid layoutId, DateOnly date)
    {
        return new HeuteBoardModel(id, ownerId, layoutId, date);
    }
}