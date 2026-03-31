using HeuteApp.Application.Results.Category;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Application.Mappers;

public static class CategoryMapper
{
    public static CategoryResult ToResult(this HeuteCategory category)
    {
        ArgumentNullException.ThrowIfNull(category);
        
        return new CategoryResult(
            category.Id,
            category.UserId,
            category.ParentId,
            category.Name
        );
    }

    public static CategoryChainResult ToChainResult(this Chain<HeuteCategory> category)
    {
        ArgumentNullException.ThrowIfNull(category);
        return new CategoryChainResult(
            category.Current.Id,
            category.Current.UserId,
            category.Current.Name,
            category.Child?.ToChainResult()
        );
    }
}