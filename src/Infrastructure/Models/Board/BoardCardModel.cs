using HeuteApp.Core.Aggregates.Board;

namespace HeuteApp.Infrastructure.Models.Board;

public class BoardCardModel : BoardCard
{
    protected BoardCardModel() { }

    protected BoardCardModel(Guid id, HeuteBoardModel? board, BoardCardProps props) : base(id, props)
    {
        BoardId = board?.Id ?? Guid.Empty;
        Board = board;
    }

    public static BoardCardModel Create(Guid id, HeuteBoardModel board, BoardCardProps props)
    {
        ArgumentNullException.ThrowIfNull(props);
        return new BoardCardModel(id, board, props);
    }

    //

    public Guid BoardId { get; private set; }

    public HeuteBoardModel? Board { get; private set; }
}