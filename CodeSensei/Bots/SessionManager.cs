using System.Collections.Concurrent;

namespace CodeSensei.Bots
{
    public class SessionManager
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
