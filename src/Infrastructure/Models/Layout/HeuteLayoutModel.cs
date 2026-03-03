using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Infrastructure.Models.Layout;

public class HeuteLayoutModel : HeuteLayout
{
    protected override LayoutSection Internal_CreateSection(LayoutSectionDefinition definition)
    {
        return LayoutSectionModel.Create(this, definition);
    }

    protected HeuteLayoutModel() { }

    protected HeuteLayoutModel(Guid ownerId, LayoutDefinition definition) : base(ownerId, definition) { }

    //

    public static new HeuteLayoutModel Create(Guid ownerId, LayoutDefinition definition)
    {
        return new HeuteLayoutModel(ownerId, definition);
    }
}