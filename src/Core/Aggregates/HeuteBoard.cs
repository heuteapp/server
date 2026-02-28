using HeuteApp.Core.Entities;
using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Core.Aggregates;

public class HeuteBoard(Guid id, Guid ownerId, Guid layoutId, DateOnly date)
{
    protected Dictionary<Guid, BoardCard> m_cards = [];

    //

    public Guid Id { get; private set; } = id;

    public Guid OwnerId { get; private set; } = ownerId;

    public Guid LayoutId { get; private set; } = layoutId;

    public DateOnly Date { get; private set; } = date;

    public IReadOnlyCollection<BoardCard> GetCards() => m_cards.Values;

    //

    public void ChangeLayout(Guid layoutId)
    {
        LayoutId = layoutId;

        UnplaceAllCards();
    }

    public BoardCard AddCard(Guid id, BoardCardProps props)
    {
        ArgumentNullException.ThrowIfNull(props);

        if (HasCard(id))
        {
            throw new InvalidOperationException($"Card with id {id} already exists.");
        }

        if(m_cards.Count >= 24)
        {
            throw new InvalidOperationException("Board already has maximum number of cards (24).");
        }

        return DoAddCard(id, props);
    }

    public void RemoveCard(Guid cardId)
    {
        if (!HasCard(cardId))
        {
            throw new InvalidOperationException($"Card with id {cardId} does not exist.");
        }

        var card = GetCards().First(c => c.Id == cardId);
        if(card != null) m_cards.Remove(card.Id);
    }

    public void PlaceCard(HeuteLayout layout, Guid cardId, Guid sectionId, GridRect position)
    {
        if (!HasCard(cardId))
        {
            throw new InvalidOperationException($"Card with id {cardId} does not exist.");
        }

        var section = layout.GetSections().FirstOrDefault(s => s.Id == sectionId) 
            ?? throw new InvalidOperationException($"Section with id {sectionId} does not exist.");

        if (!section.Size.Contains(position))
            throw new InvalidOperationException("Card is out of section bounds.");

        var conflictingCard = GetCards()
            .FirstOrDefault(c => c.Id != cardId && c.SectionId == sectionId && c.Position?.Overlaps(position) == true);

        if (conflictingCard is not null)
        {
            throw new InvalidOperationException($"Position overlaps with card {conflictingCard.Id}");
        }

        var card = GetCards().First(c => c.Id == cardId);
        card.DoPlace(sectionId, position);
    }

    public void UnplaceCard(Guid cardId)
    {
        if (!HasCard(cardId))
        {
            throw new InvalidOperationException($"Card with id {cardId} does not exist.");
        }

        var card = GetCards().First(c => c.Id == cardId);
        card.DoUnplace();
    }

    public void UnplaceAllCards()
    {
        foreach (var card in GetCards())
        {
            card.DoUnplace();
        }
    }

    public bool HasCard(Guid cardId)
    {
        return m_cards.ContainsKey(cardId);
    }

    //

    private BoardCard DoAddCard(Guid id, BoardCardProps props)
    {
        var boardCard = new BoardCard(id, props);
        m_cards.Add(id, boardCard);

        return boardCard;
    }
}