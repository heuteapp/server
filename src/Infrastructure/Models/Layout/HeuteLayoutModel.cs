using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects.Layout;
using HeuteApp.Infrastructure.Models.User;

namespace HeuteApp.Infrastructure.Models.Layout;

public class HeuteLayoutModel : HeuteLayout
{
    protected override LayoutSection Internal_CreateSection(LayoutSectionDefinition definition)
    {
        return LayoutSectionModel.Create(this, definition);
    }

    protected HeuteLayoutModel() { }

    protected HeuteLayoutModel(HeuteUserModel owner, LayoutDefinition definition) : base(new LayoutOwnership(owner.Id), definition) { }

    //

    public static HeuteLayoutModel Create(HeuteUserModel owner, LayoutDefinition definition)
    {
        return new HeuteLayoutModel(owner, definition);
    }
}