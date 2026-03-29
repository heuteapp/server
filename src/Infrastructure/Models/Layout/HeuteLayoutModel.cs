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

    protected HeuteLayoutModel(HeuteProfileModel owner, LayoutDefinition definition) : base(owner.Id, definition) 
    {
        Owner = owner;
    }

    //

    public HeuteProfileModel Owner { get; private set; } = null!;

    //

    public static HeuteLayoutModel Create(HeuteProfileModel owner, LayoutDefinition definition)
    {
        return new HeuteLayoutModel(owner, definition);
    }
}