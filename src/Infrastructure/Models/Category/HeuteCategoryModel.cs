using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects.Category;

namespace HeuteApp.Infrastructure.Models.Category;

public class HeuteCategoryModel : HeuteCategory
{
    protected HeuteCategoryModel() { }

    protected HeuteCategoryModel(CategoryDefinition definition) : base(definition) { }

    //

    public static new HeuteCategoryModel Create(CategoryDefinition definition)
    {
        return new HeuteCategoryModel(definition);
    }
}