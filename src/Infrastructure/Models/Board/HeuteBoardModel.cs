using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.ValueObjects.Board;
using HeuteApp.Infrastructure.Models.Layout;
using HeuteApp.Infrastructure.Models.User;

namespace HeuteApp.Infrastructure.Models.Board;

public class HeuteBoardModel : HeuteBoard
{    
    protected override BoardCard Internal_CreateCard(BoardCardDefinition definition)
    {
        return BoardCardModel.Create(this, definition);
    }

    protected HeuteBoardModel() { }

    protected HeuteBoardModel(HeuteUserModel user, HeuteLayoutModel layout, BoardDefinition definition) : base(new(user.Id, layout.Id), definition)
    { 
        User = user;
        Layout = layout;
    }

    //

    public HeuteUserModel User { get; private set; } = null!;

    public HeuteLayoutModel Layout { get; private set; } = null!;

    //

    public static HeuteBoardModel Create(HeuteUserModel user, HeuteLayoutModel layout, BoardDefinition definition)
    {
        return new HeuteBoardModel(user, layout, definition);
    }
}