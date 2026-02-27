using HeuteApp.Core.Aggregates;

namespace HeuteApp.Infrastructure.Models;

public class HeuteBoardModel : HeuteBoard
{
    public List<BoardCardModel> Cards { get; set; } = [];
}