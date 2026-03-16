using HeuteApp.Application.Services;
using HeuteApp.Application.Services.UserBased;

namespace HeuteApp.Application.Interfaces.UserBased;

public interface IUserBasedActionContextFactory
{
    IUserBasedActionContext Create(
        Guid userId,
        UserBasedCommandService commandService,
        CategoryService categoryService,
        BoardService boardService,
        LayoutService layoutService);
}