using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Core.Aggregates.Board;

public class HeuteBoard
{
    public static int MaxCardCount => 12;

    public static HeuteBoard Create(Guid id, Guid ownerId, Guid layoutId, DateOnly date)
    {
        return new HeuteBoard(id, ownerId, layoutId, date);
    }

    //

    private readonly List<BoardCard> m_cards = [];

    protected HeuteBoard() { }

    protected HeuteBoard(Guid id, Guid ownerId, Guid layoutId, DateOnly date)
    {
        Id = id;
        OwnerId = ownerId;
        LayoutId = layoutId;
        Date = date;
    }

    internal protected virtual BoardCard Internal_CreateCard(Guid id, BoardCardProps props)
    {
        return BoardCard.Create(id, props);
    }

    //

    public Guid Id { get; private set; }

    public Guid OwnerId { get; private set; }

    public Guid LayoutId { get; private set; }

    public DateOnly Date { get; private set; }

    public IReadOnlyCollection<BoardCard> Cards => m_cards;

    //

    public bool IsMaxCardsReached()
    {
        return Cards.Count >= MaxCardCount;
    }

    internal bool Internal_AddCard(BoardCard card)
    {
        ArgumentNullException.ThrowIfNull(card);

        if (m_cards.Any(c => c.Id == card.Id))
            return false;

        m_cards.Add(card);
        return true;
    }

    internal BoardCard Internal_RemoveCard(Guid cardId)
    {
        var card = Cards.First(c => c.Id == cardId);
        m_cards.Remove(card);
        return card;
    }
}