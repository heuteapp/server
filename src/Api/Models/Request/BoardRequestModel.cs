namespace HeuteApp.Api.Models.Request;

public record GetBoardRequest(Guid OwnerId, DateOnly Date);

public record CreateBoardRequest(Guid OwnerId, string LayoutName, int LayoutVersion);