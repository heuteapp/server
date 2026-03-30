using HeuteApp.Application.Results.Category;
using HeuteApp.Core.Aggregates.Category;

namespace HeuteApp.Application.Mappers;

public static class CategoryMapper
{
    public static CategoryResult ToResult(this HeuteCategory profile)
    {
        ArgumentNullException.ThrowIfNull(profile);
        
        return new CategoryResult(
            profile.Id,
            profile.UserId,
            profile.ParentId,
            profile.Name
        );
    }
}