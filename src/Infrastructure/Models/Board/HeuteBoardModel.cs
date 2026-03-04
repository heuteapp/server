using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.ValueObjects.Board;
using HeuteApp.Infrastructure.Models.Category;
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

    protected HeuteBoardModel(HeuteUserModel owner,  HeuteCategoryModel category, HeuteLayoutModel layout, BoardDefinition definition) : base(new(owner.Id, category.Id), new (layout.Id), definition)
    { 
        Owner = owner;
        Category = category;
        Layout = layout;
    }

    //

    public HeuteUserModel Owner { get; private set; } = null!;

    public HeuteCategoryModel Category { get; private set; } = null!;

    public HeuteLayoutModel Layout { get; private set; } = null!;

    //

    public static HeuteBoardModel Create(HeuteUserModel owner, HeuteCategoryModel category, HeuteLayoutModel layout, BoardDefinition definition)
    {
        return new HeuteBoardModel(owner, category, layout, definition);
    }
}