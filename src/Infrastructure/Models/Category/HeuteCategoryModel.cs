using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Infrastructure.Models.User;

namespace HeuteApp.Infrastructure.Models.Category;

public class HeuteCategoryModel : HeuteCategory
{
    protected HeuteCategoryModel() { }

    protected HeuteCategoryModel(HeuteUserModel owner, CategoryDefinition definition) : base(new (owner.Id), definition) 
    {
        Owner = owner;
    }

    //

    public HeuteUserModel Owner { get; private set; } = null!;

    public static HeuteCategoryModel Create(HeuteUserModel owner, CategoryDefinition definition)
    {
        return new HeuteCategoryModel(owner, definition);
    }
}