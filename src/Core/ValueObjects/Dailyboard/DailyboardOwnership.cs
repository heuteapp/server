namespace HeuteApp.Core.ValueObjects.Dailyboard;

public sealed record DailyboardOwnership
{
    public static DailyboardOwnership Empty => new(Guid.Empty, Guid.Empty);

    //

    public Guid OwnerId { get; private set; } = Guid.Empty;

    public Guid CategoryId { get; private set; } = Guid.Empty;

    //

    public DailyboardOwnership() { }
    
    public DailyboardOwnership(Guid ownerId, Guid categoryId)
    {
        OwnerId = ownerId;
        CategoryId = categoryId;
    }
}