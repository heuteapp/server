using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Infrastructure.Models.Layout;

public class HeuteLayoutModel : HeuteLayout
{
    protected override LayoutSection OnCreateSection(Guid id, string name, LayoutSectionProps props)
    {
        return LayoutSectionModel.Create(id, name, this, props);
    }

    protected HeuteLayoutModel() { }

    protected HeuteLayoutModel(Guid id, Guid ownerId, LayoutKey key) : base(id, ownerId, key) { }

    //

    public static new HeuteLayoutModel Create(Guid id, Guid ownerId, LayoutKey key)
    {
        return new HeuteLayoutModel(id, ownerId, key);
    }
}