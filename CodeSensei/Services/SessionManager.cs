using System.Collections.Concurrent;
using CodeSensei.Models;
using CodeSensei.Services.Interfaces;

namespace CodeSensei.Services
{
    public class SessionManager : ISessionManager
    {
        private ConcurrentDictionary<string, UserSessionContext> _sessions = new ConcurrentDictionary<string, UserSessionContext>();

        public UserSessionContext GetOrCreateSession(string userId)
        {
            return _sessions.GetOrAdd(userId, _ => new UserSessionContext());
        }

        public void UpdateSession(string userId, UserSessionContext context)
        {
            _sessions[userId] = context;
        }
    }
}
