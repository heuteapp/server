using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.ValueObjects.Board;
using HeuteApp.Infrastructure.Models.Layout;

namespace HeuteApp.Infrastructure.Models.Board;

public class HeuteBoardModel : HeuteBoard
{    
    protected override BoardCard Internal_CreateCard(BoardCardDefinition definition)
    {
        return BoardCardModel.Create(this, definition);
    }

    protected HeuteBoardModel() { }

    protected HeuteBoardModel(HeuteLayoutModel layout, BoardDefinition definition) : base(definition)
    { 
        Layout = layout;
    }

    //

    public HeuteLayoutModel? Layout { get; private set; }

    //

    public static HeuteBoardModel Create(HeuteLayoutModel layout, BoardDefinition definition)
    {
        return new HeuteBoardModel(layout, definition);
    }
}