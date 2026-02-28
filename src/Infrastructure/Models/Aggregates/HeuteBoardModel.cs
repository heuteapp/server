using HeuteApp.Core.Aggregates;
using HeuteApp.Core.Entities;
using HeuteApp.Infrastructure.Models.Entities;

namespace HeuteApp.Infrastructure.Models.Aggregates;

public class HeuteBoardModel : HeuteBoard
{
    protected override BoardCard OnCardInstance(Guid id, BoardCardProps props)
    {
        return new BoardCardModel(id, props);
    }

    protected override void OnAddCard(BoardCard card)
    {
        base.OnAddCard(card);
        Cards.Add((BoardCardModel)card);
    }

    override protected void OnRemoveCard(BoardCard card)
    {
        base.OnRemoveCard(card);
        Cards.RemoveAll(c => c.Id == card.Id);
    }

    //

    public List<BoardCardModel> Cards { get; set; } = [];
}