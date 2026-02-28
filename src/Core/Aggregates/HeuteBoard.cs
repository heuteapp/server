using HeuteApp.Core.Entities;
using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Core.Aggregates;

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

    public IReadOnlyCollection<BoardCard> Cards => m_cards.AsReadOnly();

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

        DoRemoveCard(cardId);
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

        var conflictingCard = Cards
            .FirstOrDefault(c => c.Id != cardId && c.SectionId == sectionId && c.Position?.Overlaps(position) == true);

        if (conflictingCard is not null)
        {
            throw new InvalidOperationException($"Position overlaps with card {conflictingCard.Id}");
        }

        var card = Cards.First(c => c.Id == cardId);
        card.DoPlace(sectionId, position);
    }

    public void UnplaceCard(Guid cardId)
    {
        if (!HasCard(cardId))
        {
            throw new InvalidOperationException($"Card with id {cardId} does not exist.");
        }

        var card = Cards.First(c => c.Id == cardId);
        card.DoUnplace();
    }

    public void UnplaceAllCards()
    {
        foreach (var card in Cards)
        {
            card.DoUnplace();
        }
    }

    public bool HasCard(Guid cardId)
    {
        return m_cards.Any(c => c.Id == cardId);
    }

    //

    private BoardCard DoAddCard(Guid id, BoardCardProps props)
    {
        var boardCard = OnCreateCard(id, props);
        m_cards.Add(boardCard);
        return boardCard;
    }

    private BoardCard DoRemoveCard(Guid cardId)
    {
        var card = Cards.First(c => c.Id == cardId);
        m_cards.Remove(card);
        return card;
    }
}