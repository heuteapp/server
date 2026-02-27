using HeuteApp.Core.Entities;

namespace HeuteApp.Infrastructure.Models;

public class BoardCardModel : BoardCard
{
    private BoardCardModel() : base()
    {
        
    }

    public BoardCardModel(Guid id, BoardCardProps props) : base(id, props)
    {
        
    }

    //

    public Guid BoardId { get; set; }

    public BoardModel? Board { get; set; } = null;
}