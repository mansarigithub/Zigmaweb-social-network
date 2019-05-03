using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Common.PushNotification
{
    public interface IPushNotificationProvider
    {
        void Send(int userId, PushNotificationType pushNotificationType, object data);
    }
}