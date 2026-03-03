using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Infrastructure.Models.Layout;

public class HeuteLayoutModel : HeuteLayout
{
    protected override LayoutSection Internal_CreateSection(Guid id, LayoutSectionKey key, LayoutSectionProps props)
    {
        return LayoutSectionModel.Create(this, id, key, props);
    }

    protected HeuteLayoutModel() { }

    protected HeuteLayoutModel(Guid id, Guid ownerId, LayoutKey key) : base(id, ownerId, key) { }

    //

    public static new HeuteLayoutModel Create(Guid id, Guid ownerId, LayoutKey key)
    {
        return new HeuteLayoutModel(id, ownerId, key);
    }
}