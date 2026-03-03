using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Infrastructure.Models.Board;

public class BoardCardModel : BoardCard
{
    protected BoardCardModel() { }

    protected BoardCardModel(HeuteBoardModel? board, BoardCardDefinition definition) : base(definition)
    {
        BoardId = board?.Id ?? Guid.Empty;
        Board = board;
    }

    public static BoardCardModel Create(HeuteBoardModel board, BoardCardDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return new BoardCardModel(board, definition);
    }

    //

    public Guid BoardId { get; private set; }

    public HeuteBoardModel? Board { get; private set; }
}