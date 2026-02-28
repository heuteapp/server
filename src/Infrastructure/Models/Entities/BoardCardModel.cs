using HeuteApp.Core.Entities;
using HeuteApp.Infrastructure.Models.Aggregates;

namespace HeuteApp.Infrastructure.Models.Entities;

public class BoardCardModel(Guid id, BoardCardProps props) : BoardCard(id, props)
{
    public Guid BoardId { get; set; }

    public HeuteBoardModel? Board { get; set; }
}