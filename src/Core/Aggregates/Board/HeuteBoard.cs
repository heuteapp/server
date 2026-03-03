using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Core.Aggregates.Board;

public class HeuteBoard
{
    public static int MaxCardCount => 12;

    public static HeuteBoard Create(BoardDefinition definition)
    {
        return new HeuteBoard(definition);
    }

    //

    private readonly List<BoardCard> m_cards = [];

    protected HeuteBoard() { }

    protected HeuteBoard(BoardDefinition definition)
    {
        Id = Guid.NewGuid();
        OwnerId = definition.OwnerId;
        LayoutId = definition.LayoutId;
        Date = definition.Key.Date;
    }

    internal protected virtual BoardCard Internal_CreateCard(BoardCardDefinition definition)
    {
        return BoardCard.Create(definition);
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