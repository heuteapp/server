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

    public static CategoryTreeResult ToTreeResult(this Tree<HeuteCategory> category)
    {
        ArgumentNullException.ThrowIfNull(category);
        return new CategoryTreeResult(
            category.Current.Id,
            category.Current.UserId,
            category.Current.Name,
            category.Children?.Select(c => c.ToTreeResult())
        );
    }

    //

    public static CategoryResult ToLastResult(this Chain<HeuteCategory> category)
    {
        ArgumentNullException.ThrowIfNull(category);
        Chain<HeuteCategory>? current = category;

        while (current.Child != null)
            current = current.Child;

        return new CategoryResult(
            current.Current.Id,
            current.Current.UserId,
            current.Current.ParentId,
            current.Current.Name
        );
    }

    public static CategoryResult ToLastResult(this CategoryChainResult chain)
    {
        ArgumentNullException.ThrowIfNull(chain);
        CategoryChainResult? parent = null;
        CategoryChainResult? current = chain;

        while (current != null)
        {
            parent = current;
            current = current.Child;
        }

        return new CategoryResult(
            current!.Id,
            current.UserId,
            parent?.Id,
            current.Name
        );
    }
}