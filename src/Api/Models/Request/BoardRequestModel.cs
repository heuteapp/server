namespace HeuteApp.Api.Models.Request;

public record CreateBoardRequest(Guid UserId, DateOnly Date);