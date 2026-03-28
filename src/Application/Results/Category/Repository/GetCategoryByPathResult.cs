using HeuteApp.Application.Enums.Results.Category.Repository;
using HeuteApp.Core.Aggregates.Category;

namespace HeuteApp.Application.Results.Category.Repository;

public record CategoryPathResult
{
    public HeuteCategory? Category { get; init; }

    public CategoryPathStatus Status { get; init; }

    public string? MissingSegment { get; init; }

    public int? MissingAtLevel { get; init; }
    
    public bool IsSuccess => Status == CategoryPathStatus.Success;
}