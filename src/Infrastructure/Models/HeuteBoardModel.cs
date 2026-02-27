using HeuteApp.Core.Aggregates;

namespace HeuteApp.Infrastructure.Models;

public class HeuteBoardModel : HeuteBoard
{
    public override ICollection<BoardCardModel> Cards { get; set; } = [];
}