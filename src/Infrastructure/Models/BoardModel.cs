namespace HeuteApp.Infrastructure.Models;

public class BoardModel
{
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }
    
    public Guid LayoutId { get; set; }

    public DateOnly Date { get; set; }

    public List<BoardCardModel> Cards { get; set; } = [];
}