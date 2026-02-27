using HeuteApp.Core.Entities;

namespace HeuteApp.Infrastructure.Models;

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