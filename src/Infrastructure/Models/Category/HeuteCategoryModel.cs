using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Infrastructure.Models.Profile;

namespace HeuteApp.Infrastructure.Models.Category;

public class HeuteCategoryModel : HeuteCategory
{
    protected HeuteCategoryModel() { }

    protected HeuteCategoryModel(HeuteProfileModel owner, HeuteCategoryModel? parent, CategoryDefinition definition) : base(new (owner.Id), definition) 
    {
        Owner = owner;
        Parent = parent;
    }

    //

    public HeuteProfileModel Owner { get; private set; } = null!;

    public HeuteCategoryModel? Parent { get; private set; } = null;

    public static HeuteCategoryModel Create(HeuteProfileModel owner, HeuteCategoryModel? parent, CategoryDefinition definition)
    {
        return new HeuteCategoryModel(owner, parent, definition);
    }
}