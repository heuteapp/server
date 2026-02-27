using HeuteApp.Core.Entities;
using HeuteApp.Infrastructure.Models.Aggregates;

namespace HeuteApp.Infrastructure.Models.Entities;

public class BoardCardModel : BoardCard
{
    private BoardCardModel() : base()
    {
        
    }

    public BoardCardModel(Guid id, Guid boardId, BoardCardProps props) : base(id, props)
    {
        BoardId = boardId;
    }

    //

    public Guid BoardId { get; set; }

    public HeuteBoardModel? Board { get; set; } = null;
}