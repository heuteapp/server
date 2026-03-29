using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Infrastructure.Models.Profile;

namespace HeuteApp.Infrastructure.Models.Category;

public class HeuteCategoryModel : HeuteCategory
{
    protected HeuteCategoryModel() { }

    protected HeuteCategoryModel(HeuteProfileModel profile, HeuteCategoryModel? parent, CategoryDefinition definition) : base(profile.Id, parent?.Id, definition) 
    {
        Profile = profile;
        Parent = parent;
    }

    //

    public HeuteProfileModel Profile { get; private set; } = null!;

    public HeuteCategoryModel? Parent { get; private set; } = null;

    public static HeuteCategoryModel Create(HeuteProfileModel profile, HeuteCategoryModel? parent, CategoryDefinition definition)
    {
        return new HeuteCategoryModel(profile, parent, definition);
    }
}