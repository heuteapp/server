using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Infrastructure.Models.Entities;

namespace HeuteApp.Infrastructure.Models.Aggregates;

public class HeuteLayoutModel : HeuteLayout
{
    protected override LayoutSection OnCreateSection(Guid id, string name, LayoutSectionProps props)
    {
        return LayoutSectionModel.Create(id, name, this, props);
    }

    protected HeuteLayoutModel() { }

    protected HeuteLayoutModel(Guid id, Guid ownerId, string name, int version) : base(id, ownerId, name, version) { }

    //

    public static new HeuteLayoutModel Create(Guid id, Guid ownerId, string name, int version)
    {
        return new HeuteLayoutModel(id, ownerId, name, version);
    }
}