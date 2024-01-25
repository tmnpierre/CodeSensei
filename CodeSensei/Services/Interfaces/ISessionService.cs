using CodeSensei.Models;

namespace CodeSensei.Services.Interfaces
{
    public interface ISessionService
    {
        UserSessionContext GetOrCreateSession(string userId);
        void UpdateSession(string userId, UserSessionContext context);
    }
}
