using HeuteApp.Core.Entities;
using HeuteApp.Infrastructure.Models.Aggregates;

namespace HeuteApp.Infrastructure.Models.Entities;

public class LayoutSectionModel : LayoutSection
{
    protected LayoutSectionModel() { }

    protected LayoutSectionModel(Guid id, string name, HeuteLayoutModel layout, LayoutSectionProps props) : base(id, name, props)
    {
        LayoutId = layout.Id;
        Layout = layout;
    }

    public static LayoutSectionModel Create(Guid id, string name, HeuteLayoutModel layout, LayoutSectionProps props)
    {        
        ArgumentNullException.ThrowIfNull(layout);
        ArgumentNullException.ThrowIfNull(props);
        return new LayoutSectionModel(id, name, layout, props);
    }

    //

    public Guid LayoutId { get; private set; }

    public HeuteLayoutModel Layout { get; private set; } = null!;
}