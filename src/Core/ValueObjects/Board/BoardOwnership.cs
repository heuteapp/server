namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardOwnership
{
    public static BoardOwnership Empty => new(Guid.Empty, Guid.Empty);

    //

    public Guid OwnerId { get; }

    public Guid CategoryId { get; }

    //

    private BoardOwnership() { }
    
    public BoardOwnership(Guid ownerId, Guid categoryId)
    {
        OwnerId = ownerId;
        CategoryId = categoryId;
    }
}