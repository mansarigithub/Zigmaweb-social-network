using Microsoft.AspNet.SignalR;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Common.PushNotification;
using ZigmaWeb.Facade.Net;
using ZigmaWeb.UI.SignalR;

namespace ZigmaWeb.UI.Infrastructure.SignalR
{
    public class SignalRPushNotificationProvider : IPushNotificationProvider
    {
        static SignalRPushNotificationProvider _instance;
        IHubContext _context;
        private SignalRPushNotificationProvider()
        {
            _context = GlobalHost.ConnectionManager.GetHubContext<UsersHub>();
        }

        public static SignalRPushNotificationProvider Instance
        {
            get
            {
                if (_instance == null) {
                    _instance = new SignalRPushNotificationProvider();
                }
                return _instance;
            }
        }

        public void Send(int userId, PushNotificationType pushNotificationType, object data)
        {
            var connections = OnlineUsers.GetUserConnections(userId);
            if (connections == null) return;
            connections.ForEach(connectionId => {
                _context.Clients.Client(connectionId).PushNotification(pushNotificationType, data);
            });
        }
    }
}