using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Core.Aggregates.Board;

public class HeuteBoard
{
    public static int MaxCardCount => 12;

    //

    private readonly List<BoardCard> m_cards = [];

    protected HeuteBoard() { }

    protected HeuteBoard(BoardOwnership ownership, BoardReference reference, BoardDefinition definition)
    {
        Id = Guid.NewGuid();
        OwnerId = ownership.OwnerId;
        CategoryId = ownership.CategoryId;
        LayoutId = reference.LayoutId;
        Date = definition.Date;
    }

    //

    public static HeuteBoard Create(BoardOwnership ownership, BoardReference reference, BoardDefinition definition)
    {
        return new HeuteBoard(ownership, reference, definition);
    }

    //

    public Guid Id { get; private set; }

    public Guid OwnerId { get; private set; }

    public Guid LayoutId { get; private set; }

    public Guid CategoryId { get; private set; }

    public DateOnly Date { get; private set; }

    public IReadOnlyCollection<BoardCard> Cards => m_cards;

    //

    public bool IsMaxCardsReached()
    {
        return Cards.Count >= MaxCardCount;
    }

    //

    internal protected virtual BoardCard Internal_CreateCard(BoardCardDefinition definition)
    {
        return BoardCard.Create(definition);
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