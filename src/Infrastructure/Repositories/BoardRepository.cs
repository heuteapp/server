using Microsoft.EntityFrameworkCore;
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Application.Interfaces;
using HeuteApp.Infrastructure.Persistence;
using HeuteApp.Infrastructure.Models.Layout;
using HeuteApp.Infrastructure.Models.Board;
using HeuteApp.Core.ValueObjects.Board;
using HeuteApp.Core.Aggregates.Profile;
using HeuteApp.Infrastructure.Models.Profile;
using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Infrastructure.Models.Category;

namespace HeuteApp.Infrastructure.Repositories;

public class BoardRepository(HeuteDbContext conext) : IBoardRepository
{
    public async Task<HeuteBoard?> GetByIdAsync(Guid boardId)
    {
        var entity = await conext.Boards
            .Include(b => b.Layout)
            .Include(b => b.Cards)
            .FirstOrDefaultAsync(b => b.Id == boardId);

        return entity;
    }

    public async Task<HeuteBoard?> GetByKeyAsync(BoardOwnership ownership, BoardKey key)
    {
        var entity = await conext.Boards
            .Include(b => b.Cards)
            .Include(b => b.Layout)
            // TODO: This is a temporary workaround until we have proper user management in place.
            .FirstOrDefaultAsync(b => b.OwnerId == ownership.OwnerId && b.CategoryId == ownership.CategoryId && b.Date == key.Date);

        return entity;
    }

    public Task<HeuteBoard> CreateAsync(HeuteProfile user, HeuteCategory category, HeuteLayout layout, BoardDefinition definition)
    {
        if(user is not HeuteProfileModel userModel)
            throw new ArgumentException("Expected HeuteProfileModel", nameof(user));

        if(category is not HeuteCategoryModel categoryModel)
            throw new ArgumentException("Expected HeuteCategoryModel", nameof(category));

        if(layout is not HeuteLayoutModel layoutModel)
            throw new ArgumentException("Expected HeuteLayoutModel", nameof(layout));

        var model = HeuteBoardModel.Create(userModel, categoryModel, layoutModel, definition);

        conext.Boards.Add(model);
        return Task.FromResult<HeuteBoard>(model);
    }
}