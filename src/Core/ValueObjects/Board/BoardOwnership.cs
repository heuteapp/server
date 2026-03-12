namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardOwnership
{
    public static BoardOwnership Empty => new(Guid.Empty, Guid.Empty);

    //

    public Guid OwnerId { get; } = Guid.Empty;

    public Guid CategoryId { get; } = Guid.Empty;

    //

    private BoardOwnership() { }
    
    public BoardOwnership(Guid ownerId, Guid categoryId)
    {
        OwnerId = ownerId;
        CategoryId = categoryId;
    }
}