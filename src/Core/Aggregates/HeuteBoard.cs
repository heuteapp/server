using HeuteApp.Core.Entities;
using HeuteApp.Core.Mappers;
using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Core.Aggregates;

public class HeuteBoard
{
    private readonly Guid m_id;

    private readonly Guid m_ownerId;

    private Guid m_layoutId;

    private readonly DateOnly m_date;

    private readonly Dictionary<Guid, BoardCard> m_cardDictionary = [];

    private HeuteBoard() { } 

    private HeuteBoard(Guid id, Guid ownerId, Guid layoutId, DateOnly date, HeuteBoardProps props)
    {
        ArgumentNullException.ThrowIfNull(props);

        m_id = id;
        m_ownerId = ownerId;
        m_layoutId = layoutId;
        m_date = date;

        if (props != null)
        {
            foreach (var card in props.Cards)
            {
                DoAddCard(card.Id, card.ToProps());
            }
        }
    }

    public static HeuteBoard Create(Guid id, Guid ownerId, Guid layoutId, DateOnly date, HeuteBoardProps props)
        => new(id, ownerId, layoutId, date, props);

    //

    public Guid Id => m_id;
    
    public Guid OwnerId => m_ownerId;

    public Guid LayoutId => m_layoutId;

    public DateOnly Date => m_date;

    public IReadOnlyCollection<BoardCard> Cards => m_cardDictionary.Values;

    //

    public void ChangeLayout(Guid layoutId)
    {
        m_layoutId = layoutId;

        UnplaceAllCards();
    }

    public void AddCard(Guid id, BoardCardProps props)
    {
        ArgumentNullException.ThrowIfNull(props);

        if (HasCard(id))
        {
            throw new InvalidOperationException($"Card with id {id} already exists.");
        }

        if(m_cardDictionary.Count >= 24)
        {
            throw new InvalidOperationException("Board already has maximum number of cards (24).");
        }

        DoAddCard(id, props);
    }

    public void RemoveCard(Guid cardId)
    {
        if (!HasCard(cardId))
        {
            throw new InvalidOperationException($"Card with id {cardId} does not exist.");
        }

        m_cardDictionary.Remove(cardId);
    }

    public void PlaceCard(HeuteLayout layout, Guid cardId, Guid sectionId, GridRect position)
    {
        if (!HasCard(cardId))
        {
            throw new InvalidOperationException($"Card with id {cardId} does not exist.");
        }

        var section = layout.Sections.FirstOrDefault(s => s.Id == sectionId) 
            ?? throw new InvalidOperationException($"Section with id {sectionId} does not exist.");

        if (!section.Size.Contains(position))
            throw new InvalidOperationException("Card is out of section bounds.");

        var conflictingCard = m_cardDictionary.Values
            .FirstOrDefault(c => c.Id != cardId && c.SectionId == sectionId && c.Position?.Overlaps(position) == true);

        if (conflictingCard is not null)
        {
            throw new InvalidOperationException($"Position overlaps with card {conflictingCard.Id}");
        }

        var card = m_cardDictionary[cardId];
        card.DoPlace(sectionId, position);
    }

    public void UnplaceCard(Guid cardId)
    {
        if (!HasCard(cardId))
        {
            throw new InvalidOperationException($"Card with id {cardId} does not exist.");
        }

        var card = m_cardDictionary[cardId];
        card.DoUnplace();
    }

    public void UnplaceAllCards()
    {
        foreach (var card in m_cardDictionary.Values)
        {
            card.DoUnplace();
        }
    }

    public bool HasCard(Guid cardId)
    {
        return m_cardDictionary.ContainsKey(cardId);
    }

    //

    private void DoAddCard(Guid id, BoardCardProps props)
    {
        var boardCard = BoardCard.Create(id, props);
        m_cardDictionary.Add(boardCard.Id, boardCard);
    }
}

public sealed record HeuteBoardProps(
    IEnumerable<BoardCard> Cards
);