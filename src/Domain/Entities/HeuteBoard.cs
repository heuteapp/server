using HeuteApp.Domain.ValueObjects;

namespace HeuteApp.Domain.Entities;

public class HeuteBoard
{
    private readonly Guid m_id;

    private readonly Guid m_ownerId;

    private Guid m_layoutId;

    private readonly Dictionary<Guid, HeuteBoardCard> m_cardDictionary = [];

    public HeuteBoard(Guid id, Guid ownerId, Guid layoutId, HeuteBoardProps props)
    {
        ArgumentNullException.ThrowIfNull(props);

        m_id = id;
        m_ownerId = ownerId;
        m_layoutId = layoutId;

        if (props != null)
        {
            foreach (var card in props.Cards)
            {
                DoAddCard(card.Id, card.Props);
            }
        }
    }

    //

    public Guid Id => m_id;
    
    public Guid OwnerId => m_ownerId;

    public Guid LayoutId => m_layoutId;

    public IReadOnlyCollection<HeuteBoardCard> Cards => m_cardDictionary.Values;

    //

    public void ChangeLayout(HeuteLayoutSnapshot layout)
    {
        ArgumentNullException.ThrowIfNull(layout);
        m_layoutId = layout.Id;
    }

    public void AddCard(Guid id, HeuteBoardCardProps props)
    {
        ArgumentNullException.ThrowIfNull(props);

        if (HasCard(id))
        {
            throw new InvalidOperationException($"Card with id {id} already exists.");
        }

        DoAddCard(id, props);
    }

    public void PlaceCard(Guid cardId, Guid sectionId, GridRect position)
    {
        if (!HasCard(cardId))
        {
            throw new InvalidOperationException($"Card with id {cardId} does not exist.");
        }

        var card = m_cardDictionary[cardId];
        card.DoPlace(sectionId, position);
    }

    public bool HasCard(Guid cardId)
    {
        return m_cardDictionary.ContainsKey(cardId);
    }

    //

    private void DoAddCard(Guid id, HeuteBoardCardProps props)
    {
        var boardCard = HeuteBoardCard.FromProps(id, props);
        m_cardDictionary.Add(boardCard.Id, boardCard);
    }

    //

    public HeuteBoardSnapshot ToSnapshot()
    {
        return new HeuteBoardSnapshot(
            Id,
            OwnerId,
            LayoutId,
            new HeuteBoardProps(
                Cards.Select(c => c.ToSnapshot())
            )
        );
    }

    public HeuteBoardProps ToProps()
    {
        return new HeuteBoardProps(
            Cards.Select(c => c.ToSnapshot())
        );
    }

    public static HeuteBoard FromSnapshot(HeuteBoardSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        return new HeuteBoard(snapshot.Id, snapshot.OwnerId, snapshot.LayoutId, snapshot.Props);
    }

    public static HeuteBoard FromProps(Guid id, Guid ownerId, Guid layoutId, HeuteBoardProps props)
    {
        ArgumentNullException.ThrowIfNull(props);

        return new HeuteBoard(id, ownerId, layoutId, props);
    }
}

public sealed record HeuteBoardSnapshot(
    Guid Id,
    Guid OwnerId,
    Guid LayoutId,
    HeuteBoardProps Props
);

public sealed record HeuteBoardProps(
    IEnumerable<HeuteBoardCardSnapshot> Cards
);