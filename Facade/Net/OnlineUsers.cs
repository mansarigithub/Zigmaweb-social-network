using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ZigmaWeb.Facade.Net
{
    public class OnlineUsers
    {
        static ConcurrentDictionary<int, List<string>> _onlineUsers;
        static OnlineUsers()
        {
            _onlineUsers = new ConcurrentDictionary<int, List<string>>();
        }

        public static void SetUserAsOnline(int userId, string connectionId)
        {
            List<string> userConnections;
            if(_onlineUsers.TryGetValue(userId, out userConnections)) {
                if (userConnections.Contains(connectionId))
                    return;
                userConnections.Add(connectionId);
                return;
            }

            _onlineUsers.TryAdd(userId, new List<string>(3) { connectionId });
        }

        public static void SetUserAsOffline(int userId, string connectionId)
        {
            List<string> userConnections;
            if(_onlineUsers.TryGetValue(userId, out userConnections)) {
                userConnections.Remove(connectionId);
            }
        }

        public static List<string> GetUserConnections(int userId)
        {
            List<string> connections = null;
            _onlineUsers.TryGetValue(userId, out connections);
            return connections;
        }
    }
}
