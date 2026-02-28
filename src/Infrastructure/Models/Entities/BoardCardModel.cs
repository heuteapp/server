using HeuteApp.Core.Entities;
using HeuteApp.Infrastructure.Models.Aggregates;

namespace HeuteApp.Infrastructure.Models.Entities;

public class BoardCardModel : BoardCard
{
    public Guid BoardId { get; set; }

    public HeuteBoardModel? Board { get; set; }
}