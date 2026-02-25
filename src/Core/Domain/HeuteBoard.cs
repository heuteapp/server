namespace HeuteApp.Core.Domain;

public class HeuteBoard
{
    private readonly Guid m_id;

    private readonly Guid m_ownerId;

    private HeuteLayoutSnapshot m_layout;

    private HeuteBoardCard[]? m_cards;

    private readonly Dictionary<Guid, HeuteBoardCard> m_cardDictionary = [];

    public HeuteBoard(Guid id, Guid ownerId, HeuteLayoutSnapshot layout, HeuteBoardProps? props = null)
    {
        m_id = id;
        m_ownerId = ownerId;
        m_layout = layout;

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

    public HeuteLayoutSnapshot Layout => m_layout;

    public HeuteBoardCard[] Cards => m_cards ??= [.. m_cardDictionary.Values];

    //

    public void ChangeLayout(HeuteLayoutSnapshot layout)
    {
        ArgumentNullException.ThrowIfNull(layout);

        m_layout = layout;
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

    public bool HasCard(Guid cardId)
    {
        return m_cardDictionary.ContainsKey(cardId);
    }

    //

    private void DoAddCard(Guid id, HeuteBoardCardProps props)
    {
        var boardCard = HeuteBoardCard.FromProps(id, props);
        m_cardDictionary.Add(boardCard.Id, boardCard);
        m_cards = null;
    }

    //

    public HeuteBoardSnapshot ToSnapshot()
    {
        return new HeuteBoardSnapshot(
            Id,
            OwnerId,
            Layout,
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

        var board = new HeuteBoard(snapshot.Id, snapshot.OwnerId, snapshot.Layout);

        foreach (var card in snapshot.Props.Cards)
        {
            board.DoAddCard(card.Id, card.Props);
        }

        return board;
    }

    public static HeuteBoard FromProps(Guid id, Guid ownerId, HeuteLayoutSnapshot layout, HeuteBoardProps props)
    {
        ArgumentNullException.ThrowIfNull(layout);
        ArgumentNullException.ThrowIfNull(props);

        var board = new HeuteBoard(id, ownerId, layout);

        foreach (var card in props.Cards)
        {
            board.DoAddCard(card.Id, card.Props);
        }

        return board;
    }
}

public sealed record HeuteBoardSnapshot(
    Guid Id,
    Guid OwnerId,
    HeuteLayoutSnapshot Layout,
    HeuteBoardProps Props
);

public sealed record HeuteBoardProps(
    IEnumerable<HeuteBoardCardSnapshot> Cards
);