namespace HeuteApp.Core.Domain;

public class HeuteBoard(Guid id, Guid ownerId, HeuteLayoutSnapshot layout)
{
    private HeuteLayoutSnapshot m_layout = layout;

    private HeuteBoardCard[]? m_cards = null;

    private readonly Dictionary<Guid, HeuteBoardCard> m_cardDictionary = [];

    //

    public Guid Id => id;
    
    public Guid OwnerId => ownerId;

    public HeuteLayoutSnapshot Layout => m_layout;

    public HeuteBoardCard[] Cards => m_cards ??= [.. m_cardDictionary.Values];

    //

    public void ChangeLayout(HeuteLayoutSnapshot layout)
    {
        ArgumentNullException.ThrowIfNull(layout);

        m_layout = layout;
    }

    public void AddCard(HeuteBoardCardSnapshot card)
    {
        ArgumentNullException.ThrowIfNull(card);

        if (HasCard(card.Id))
        {
            throw new InvalidOperationException($"Card with id {card.Id} already exists.");
        }

        DoAddCard(card);
    }

    public bool HasCard(Guid cardId)
    {
        return m_cardDictionary.ContainsKey(cardId);
    }

    //

    private void DoAddCard(HeuteBoardCardSnapshot card)
    {
        var boardCard = HeuteBoardCard.FromSnapshot(card);
        m_cardDictionary.Add(boardCard.Id, boardCard);
        m_cards = null;
    }
}