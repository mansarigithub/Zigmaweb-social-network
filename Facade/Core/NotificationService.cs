using ZigmaWeb.Bussines.Core;
using ZigmaWeb.Security.Helper;
using System.Linq;
using System.Data.Entity;
using ZigmaWeb.Common.Data;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Common.ExtensionMethod;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.Security.Identity;
using ZigmaWeb.UnitOfWork;
using System;
using System.Collections;
using ZigmaWeb.PresentationModel.Model;
using System.Collections.Generic;
using ZigmaWeb.Mapper.Core;
using Common.Exception;
using ZigmaWeb.Localization.ExtensionMethod;

namespace ZigmaWeb.Facade.Core
{
    public class NotificationService
    {
        CoreUnitOfWork UnitOfWork { get; set; }
        NotificationBiz NotificationBiz { get; set; }
        UserBiz UserBiz { get; set; }
        ContentBiz ContentBiz { get; set; }

        public NotificationService()
        {
            UnitOfWork = new CoreUnitOfWork();
            NotificationBiz = new NotificationBiz(UnitOfWork);
            UserBiz = new UserBiz(UnitOfWork);
            ContentBiz = new ContentBiz(UnitOfWork);
        }

        public void SetNotificationAsRead(UserIdentity user, int notificationId)
        {
            var notification = NotificationBiz.ReadSingle(n => n.Id == notificationId);
            if (notification.ReceiverId != user.UserId)
                throw new BusinessException();
            notification.IsRead = true;
            UnitOfWork.SaveChanges();
        }

        public IEnumerable<NotificationPM> ReadUserNotifications(int userId, int maxCount)
        {
            return NotificationBiz.ReadUserNotifications(userId, maxCount)
                .ToList()
                .Select(notification => NotificationBiz.ProcessNotification(notification).GetNotificationPM());
        }
    }
}
