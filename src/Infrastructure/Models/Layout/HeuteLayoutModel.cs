using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Infrastructure.Models.Layout;

public class HeuteLayoutModel : HeuteLayout
{
    protected override LayoutSection Internal_CreateSection(LayoutSectionDefinition definition)
    {
        return LayoutSectionModel.Create(this, definition);
    }

    protected HeuteLayoutModel(LayoutDefinition definition) : base(definition) { }

    //

    public static new HeuteLayoutModel Create(LayoutDefinition definition)
    {
        return new HeuteLayoutModel(definition);
    }
}