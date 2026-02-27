using HeuteApp.Infrastructure.Models.Entities;

namespace HeuteApp.Infrastructure.Models.Aggregates;

public class HeuteBoardModel
{
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }

    public Guid LayoutId { get; set; }

    public DateOnly Date { get; set; }

    public List<BoardCardModel> Cards { get; set; } = [];
}