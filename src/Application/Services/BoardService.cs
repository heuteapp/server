using HeuteApp.Core.Entities;
using HeuteApp.Application.Interfaces;

namespace HeuteApp.Application.Services;

public class BoardService(IBoardRepository repository)
{
    public async Task CreateBoardAsync(Guid ownerId, string title, DateOnly date)
    {
        var existing = await repository
            .GetByDateAsync(ownerId, date);

        if (existing != null)
            throw new Exception("Board already exists for this date.");

        var board = new HeuteBoard(Guid.NewGuid(), ownerId, Guid.Empty, date, new HeuteBoardProps(Cards: []));

        await repository.AddAsync(board);
    }
}