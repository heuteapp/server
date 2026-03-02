using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Core.Aggregates.Board;

public class HeuteBoard
{
    private readonly List<BoardCard> m_cards = [];

    protected virtual BoardCard OnCreateCard(Guid id, BoardCardProps props)
    {
        return BoardCard.Create(id, props);
    }

    protected HeuteBoard() { }

    protected HeuteBoard(Guid id, Guid ownerId, Guid layoutId, DateOnly date)
    {
        Id = id;
        OwnerId = ownerId;
        LayoutId = layoutId;
        Date = date;
    }

    public static HeuteBoard Create(Guid id, Guid ownerId, Guid layoutId, DateOnly date)
    {
        return new HeuteBoard(id, ownerId, layoutId, date);
    }

    //

    public Guid Id { get; private set; }

    public Guid OwnerId { get; private set; }

    public Guid LayoutId { get; private set; }

    public DateOnly Date { get; private set; }

    public IReadOnlyCollection<BoardCard> Cards => m_cards;

    //

    internal BoardCard Internal_AddCard(Guid id, BoardCardProps props)
    {
        var card = OnCreateCard(id, props);
        m_cards.Add(card);
        return card;
    }

    internal BoardCard Internal_RemoveCard(Guid cardId)
    {
        var card = Cards.First(c => c.Id == cardId);
        m_cards.Remove(card);
        return card;
    }
}