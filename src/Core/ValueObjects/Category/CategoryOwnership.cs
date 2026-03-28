namespace HeuteApp.Core.ValueObjects.Category;

public sealed record CategoryOwnership
{
    public static CategoryOwnership Empty => new();

    //

    public Guid OwnerId { get; private set; } = Guid.Empty;

    public Guid? ParentId { get; private set; } = null;

    //

    private CategoryOwnership() { }

    public CategoryOwnership(Guid ownerId)
    {
        OwnerId = ownerId;
    }
}