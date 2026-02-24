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
}