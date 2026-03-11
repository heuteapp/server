using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Core.ValueObjects.Profile;

namespace HeuteApp.Infrastructure.Models.Profile;

public class HeuteProfileModel : HeuteProfile
{
    protected HeuteProfileModel() { }

    protected HeuteProfileModel(ProfileDefinition definition) : base(definition) { }

    //

    public static new HeuteProfileModel Create(ProfileDefinition definition)
    {
        return new HeuteProfileModel(definition);
    }
}