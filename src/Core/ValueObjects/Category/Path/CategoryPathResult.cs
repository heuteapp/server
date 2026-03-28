using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Core.Enums.Category.Path;

namespace HeuteApp.Core.ValueObjects.Category.Path;

public record CategoryPathResult
{
    public HeuteCategory? Category { get; init; }

    public CategoryPathStatus Status { get; init; }

    public string? MissingSegment { get; init; }

    public int? MissingAtLevel { get; init; }
    
    public bool IsSuccess => Status == CategoryPathStatus.Success;
}