using HeuteApp.Api.Models.Responses.Category;
using HeuteApp.Application.Results.Category;

namespace HeuteApp.Api.Mappers;

public static class CategoryModelMapper
{
    public static CategoryChainResponse? ToResponse(this CategoryChainResult result)
    {
        if (result == null)
            return null;

        return new CategoryChainResponse(
            result.Name,
            result.Child?.ToResponse()
        );
    }

    public static CategoryTreeResponse? ToResponse(this CategoryTreeResult result)
    {
        if (result == null)
            return null;

        return new CategoryTreeResponse(
            result.Name,
            result.Children?.Select(c => c.ToResponse()!)
        );
    }

    public static CategoryHierarchyResponse ToResponse(this CategoryHierarchyResult result)
    {
        ArgumentNullException.ThrowIfNull(result);
        return new CategoryHierarchyResponse(
            [.. result.Roots.Select(r => r.ToResponse()!)]
        );
    }
}