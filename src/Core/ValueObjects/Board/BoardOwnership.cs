namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardOwnership
{
    public static BoardOwnership Empty => new(Guid.Empty, Guid.Empty);

    //

    public Guid OwnerId { get; private set; } = Guid.Empty;

    public Guid CategoryId { get; private set; } = Guid.Empty;

    //

    public BoardOwnership() { }
    
    public BoardOwnership(Guid ownerId, Guid categoryId)
    {
        OwnerId = ownerId;
        CategoryId = categoryId;
    }
}