using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Infrastructure.Models.Profile;

public class HeuteProfileModel : HeuteProfile
{
    protected HeuteProfileModel() { }

    protected HeuteProfileModel(ProfileOwnership ownership, ProfileDefinition definition) : base(ownership, definition) { }

    //

    public static new HeuteProfileModel Create(ProfileOwnership ownership, ProfileDefinition definition)
    {
        return new HeuteProfileModel(ownership, definition);
    }
}