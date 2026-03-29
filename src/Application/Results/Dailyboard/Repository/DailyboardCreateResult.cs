using HeuteApp.Core.Aggregates.Dailyboard;
using HeuteApp.Application.Enums.Results.Dailyboard.Repository;

namespace HeuteApp.Application.Results.Dailyboard.Repository;

public record DailyboardCreateResult
{
    public HeuteDailyboard? Dailyboard { get; init; }
    
    public DailyboardCreateStatus Status { get; init; }

    public string? ErrorMessage { get; init; }
    
    public bool IsSuccess => Status == DailyboardCreateStatus.Success;
}