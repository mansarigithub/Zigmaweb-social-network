using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using System;
using ZigmaWeb.Common.Enum;
using System.Linq;
using ZigmaWeb.Common.ExtensionMethod;
using ZigmaWeb.Localization.ExtensionMethod;

namespace ZigmaWeb.Bussines.Core
{
    public class NotificationBiz : BizBase<Notification>
    {
        public NotificationBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {

        }

        public Notification Add(NotificationType type, int? subjectId, int? objectId, int receiverId)
        {
            return Add(new Notification() {
                CreateDate = DateTime.Now,
                Type = type,
                SubjectId = subjectId,
                ObjectId = objectId,
                ReceiverId = receiverId,
            });
        }

        public IQueryable<Notification> ReadUserNotifications(int userId, int maxCount)
        {
            return Read(n => n.ReceiverId == userId)
                .Take(maxCount)
                .OrderByDescending(n => n.CreateDate);
        }

        public Notification ProcessNotification(Notification notification)
        {
            switch (notification.Type) {
                case NotificationType.WriteComment:
                    ProcessWriteCommentNotification(notification);
                    break;
                case NotificationType.WriteMessage:
                    ProcessWriteMessageNotification(notification);
                    break;
                default:
                    break;
            }
            return notification;
        }

        void ProcessWriteCommentNotification(Notification notification)
        {
            var contentTitle = UnitOfWork.ContentRepository.Read(c => c.Id == notification.ObjectId.Value)
                .Select(c => c.Title)
                .Single()
                .MakeShort(30);
            notification.Title = UnitOfWork.UserRepository.Read(u => u.Id == notification.SubjectId.Value)
                .Select(u => new { u.FirstName, u.LastName })
                .ToList()
                .Select(u => $"{u.FirstName} {u.LastName}")
                .Single()
                .MakeShort(20);
            notification.Message = string.Format("NotificationMessage_WriteComment".Localize(), contentTitle);
            notification.Url = "/user/comment";
            notification.ImageUrl = $"/visitorservice/getprofileimage/{notification.SubjectId}/small/";
        }

        void ProcessWriteMessageNotification(Notification notification)
        {
            var sender = UnitOfWork.UserRepository.Read(u => u.Id == notification.SubjectId).Select(u => new {
                u.FirstName,
                u.LastName
            }).Single();
            notification.Title = string.Format("{0} {1}", sender.FirstName, sender.LastName).MakeShort(20);
            notification.Message = "NotificationMessage_WriteMessage".Localize();
            notification.Url = "/user/message";
            notification.ImageUrl = $"/visitorservice/getprofileimage/{notification.SubjectId}/small/";
        }

    }
}