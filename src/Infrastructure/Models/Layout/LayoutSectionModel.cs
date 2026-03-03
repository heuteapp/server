using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Infrastructure.Models.Layout;

public class LayoutSectionModel : LayoutSection
{
    protected LayoutSectionModel() { }

    protected LayoutSectionModel(HeuteLayoutModel layout, Guid id, LayoutSectionKey key,LayoutSectionProps props) : base(id, key, props)
    {        
        Layout = layout;
        LayoutId = layout.Id;
    }

    public static LayoutSectionModel Create(HeuteLayoutModel layout, Guid id, LayoutSectionKey key, LayoutSectionProps props)
    {        
        ArgumentNullException.ThrowIfNull(layout);
        ArgumentNullException.ThrowIfNull(props);
        return new LayoutSectionModel(layout, id, key, props);
    }

    //

    public Guid LayoutId { get; private set; }

    public HeuteLayoutModel Layout { get; private set; } = null!;
}