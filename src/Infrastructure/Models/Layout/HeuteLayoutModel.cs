using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects.Layout;
using HeuteApp.Infrastructure.Models.Profile;

namespace HeuteApp.Infrastructure.Models.Layout;

public class HeuteLayoutModel : HeuteLayout
{
    protected override LayoutSection Internal_CreateSection(LayoutSectionDefinition definition)
    {
        return LayoutSectionModel.Create(this, definition);
    }

    protected HeuteLayoutModel() { }

    protected HeuteLayoutModel(HeuteProfileModel? profile, LayoutDefinition definition) : base(profile?.Id, definition) 
    {
        Profile = profile;
    }

    //

    public HeuteProfileModel? Profile { get; private set; } = null!;

    //

    public static HeuteLayoutModel Create(HeuteProfileModel? profile, LayoutDefinition definition)
    {
        return new HeuteLayoutModel(profile, definition);
    }
}