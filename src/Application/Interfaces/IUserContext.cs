namespace HeuteApp.Application.Interfaces;

public interface IUserContext
{
    Guid? UserId { get; }

    void SetUser(Guid userId);
}