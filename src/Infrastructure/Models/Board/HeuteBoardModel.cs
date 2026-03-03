using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.ValueObjects.Board;
using HeuteApp.Infrastructure.Models.Layout;

namespace HeuteApp.Infrastructure.Models.Board;

public class HeuteBoardModel : HeuteBoard
{    
    protected override BoardCard Internal_CreateCard(Guid id, BoardCardProps props)
    {
        return BoardCardModel.Create(id, this, props);
    }

    protected HeuteBoardModel() { }

    protected HeuteBoardModel(Guid id, Guid ownerId, HeuteLayoutModel layout, DateOnly date) : base(id, ownerId, layout.Id, date) 
    { 
        Layout = layout;
    }

    //

    public HeuteLayoutModel? Layout { get; private set; }

    //

    public static HeuteBoardModel Create(Guid id, Guid ownerId, HeuteLayoutModel layout, DateOnly date)
    {
        return new HeuteBoardModel(id, ownerId, layout, date);
    }
}