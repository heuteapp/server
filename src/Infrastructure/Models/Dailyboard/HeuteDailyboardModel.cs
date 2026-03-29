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

    protected HeuteDailyboardModel(HeuteProfileModel profile,  HeuteCategoryModel category, HeuteLayoutModel layout, DailyboardDefinition definition) : base(profile.Id, category.Id, layout.Id, definition)
    { 
        Profile = profile;
        Category = category;
        Layout = layout;
    }

    //

    public HeuteProfileModel Profile { get; private set; } = null!;

    public HeuteCategoryModel Category { get; private set; } = null!;

    public HeuteLayoutModel Layout { get; private set; } = null!;

    //

    public static HeuteDailyboardModel Create(HeuteProfileModel profile, HeuteCategoryModel category, HeuteLayoutModel layout, DailyboardDefinition definition)
    {
        return new HeuteDailyboardModel(profile, category, layout, definition);
    }
}