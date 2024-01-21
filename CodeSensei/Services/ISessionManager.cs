using CodeSensei.Models;

namespace CodeSensei.Services
{
    public interface ISessionManager
    {
        UserSessionContext GetOrCreateSession(string userId);
        void UpdateSession(string userId, UserSessionContext context);
    }
}
