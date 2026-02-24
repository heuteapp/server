namespace HeuteApp.Core.Domain;

public class HeuteBoard(Guid id, Guid ownerId, HeuteLayoutSnapshot layout)
{
    private HeuteLayoutSnapshot m_layout = layout;

    //

    public Guid Id => id;
    
    public Guid OwnerId => ownerId;

    public HeuteLayoutSnapshot Layout => m_layout;

    //

    public void ChangeLayout(HeuteLayoutSnapshot layout)
    {
        ArgumentNullException.ThrowIfNull(layout);

        m_layout = layout;
    }
}