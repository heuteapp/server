namespace HeuteApp.Infrastructure.Persistence.Entities;

public class BoardEntity
{
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }
    
    public Guid LayoutId { get; set; }
}