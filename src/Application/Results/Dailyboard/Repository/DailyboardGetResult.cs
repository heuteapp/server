using HeuteApp.Core.Aggregates.Dailyboard;
using HeuteApp.Application.Enums.Results.Dailyboard.Repository;

namespace HeuteApp.Application.Results.Dailyboard.Repository;

public record DailyboardGetResult
{
    public HeuteDailyboard? Dailyboard { get; init; }
    
    public DailyboardGetStatus Status { get; init; }
    
    public bool IsSuccess => Status == DailyboardGetStatus.Success;
}