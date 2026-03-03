using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Infrastructure.Models.Layout;

public class LayoutSectionModel : LayoutSection
{
    protected LayoutSectionModel() { }

    protected LayoutSectionModel(HeuteLayoutModel layout, LayoutSectionDefinition definition) : base(definition)
    {        
        Layout = layout;
        LayoutId = layout.Id;
    }

    public static LayoutSectionModel Create(HeuteLayoutModel layout, LayoutSectionDefinition definition)
    {        
        ArgumentNullException.ThrowIfNull(layout);
        ArgumentNullException.ThrowIfNull(definition);
        return new LayoutSectionModel(layout, definition);
    }

    //

    public Guid LayoutId { get; private set; }

    public HeuteLayoutModel Layout { get; private set; } = null!;
}