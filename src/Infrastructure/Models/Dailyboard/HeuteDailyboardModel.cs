using HeuteApp.Core.Aggregates.Dailyboard;
using HeuteApp.Core.ValueObjects.Dailyboard;
using HeuteApp.Infrastructure.Models.Category;
using HeuteApp.Infrastructure.Models.Layout;
using HeuteApp.Infrastructure.Models.Profile;

namespace HeuteApp.Infrastructure.Models.Dailyboard;

public class HeuteDailyboardModel : HeuteDailyboard
{    
    protected override DailyboardCard Internal_CreateCard(DailyboardCardDefinition definition)
    {
        return DailyboardCardModel.Create(this, definition);
    }

    protected HeuteDailyboardModel() { }

    protected HeuteDailyboardModel(HeuteProfileModel owner,  HeuteCategoryModel category, HeuteLayoutModel layout, DailyboardDefinition definition) : base(new(owner.Id, category.Id), new (layout.Id), definition)
    { 
        Owner = owner;
        Category = category;
        Layout = layout;
    }

    //

    public HeuteProfileModel Owner { get; private set; } = null!;

    public HeuteCategoryModel Category { get; private set; } = null!;

    public HeuteLayoutModel Layout { get; private set; } = null!;

    //

    public static HeuteDailyboardModel Create(HeuteProfileModel owner, HeuteCategoryModel category, HeuteLayoutModel layout, DailyboardDefinition definition)
    {
        return new HeuteDailyboardModel(owner, category, layout, definition);
    }
}