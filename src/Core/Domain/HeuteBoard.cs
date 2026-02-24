namespace HeuteApp.Core.Domain;

public class HeuteBoard(Guid id, Guid ownerId)
{
    public Guid Id => id;
    
    public Guid OwnerId => ownerId;
}