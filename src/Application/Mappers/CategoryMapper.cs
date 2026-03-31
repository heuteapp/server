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

    public static CategoryChainResult ToResult(this Chain<HeuteCategory> category)
    {
        ArgumentNullException.ThrowIfNull(category);
        return new CategoryChainResult(
            category.Current.Id,
            category.Current.UserId,
            category.Current.Name,
            category.Child?.ToResult()
        );
    }

    public static CategoryTreeResult ToResult(this Tree<HeuteCategory> category)
    {
        ArgumentNullException.ThrowIfNull(category);
        return new CategoryTreeResult(
            category.Current.Id,
            category.Current.UserId,
            category.Current.Name,
            category.Children?.Select(c => c.ToResult())
        );
    }

    public static CategoryHierarchyResult ToResult(this Hierarchy<HeuteCategory> hierarchy)
    {
        ArgumentNullException.ThrowIfNull(hierarchy);
        return new CategoryHierarchyResult(
            hierarchy.Roots.Select(tree => tree.ToResult())
        );
    }

    //

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