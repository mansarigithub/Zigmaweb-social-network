using System.Collections.Generic;
using System.Linq;
using ZigmaWeb.Bussines.Core;
using ZigmaWeb.Common.Data;
using ZigmaWeb.Common.ExtensionMethod;
using ZigmaWeb.Mapper.Core;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.Security.Identity;
using ZigmaWeb.UnitOfWork;
using System.Data.Entity;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Common.PushNotification;

namespace ZigmaWeb.Facade.Core
{
    public class CommentService
    {
        IPushNotificationProvider PushNotification { get; set; }
        CoreUnitOfWork UnitOfWork { get; set; }
        CommentBiz CommentBiz { get; set; }
        NotificationBiz NotificationBiz { get; set; }
        ContentBiz ContentBiz { get; set; }

        public CommentService(IPushNotificationProvider pushNotificationProvider)
        {
            UnitOfWork = new CoreUnitOfWork();
            CommentBiz = new CommentBiz(UnitOfWork);
            NotificationBiz = new NotificationBiz(UnitOfWork);
            ContentBiz = new ContentBiz(UnitOfWork);
            PushNotification = pushNotificationProvider;
        }

        public void AddComment(CommentRegistrationPM commentRegistrationPM, UserIdentity userIdentity)
        {
            Notification notification = null;
            var comment = commentRegistrationPM.GetComment();
            var contentAuthorId = ContentBiz.Read(c => c.Id == comment.ContentId).Select(c => c.AuthorId).Single();
            comment.SenderId = userIdentity.UserId;
            CommentBiz.AddComment(comment);
            if (userIdentity.UserId != contentAuthorId)
                notification = NotificationBiz.Add(NotificationType.WriteComment, userIdentity.UserId, comment.ContentId, contentAuthorId);
            UnitOfWork.SaveChanges();
            commentRegistrationPM.Id = comment.Id;
            commentRegistrationPM.CreateDate = comment.CreateDate;
            if (notification != null)
                PushNotification.Send(contentAuthorId, PushNotificationType.NewNotification, NotificationBiz.ProcessNotification(notification).GetNotificationPM());
        }

        public DataSourceResult ReadCommentsForViewByOwner(DataSourceRequest request, UserIdentity user)
        {
            return CommentBiz
                .Read(comment => comment.Content.AuthorId == user.UserId, noTracking: true)
                .OrderByDescending(comment => comment.CreateDate)
                .Include(comment => comment.Sender)
                .Include(comment => comment.Content)
                .MapTo<CommentInfoPM>()
                .ToDataSourceResult(request);
        }

        public void DeleteComments(IEnumerable<int> ids, UserIdentity user)
        {
            //TODO: put here a security check
            var comments = ids.Select(id => new Comment() { Id = id });
            comments.ForEach(comment => CommentBiz.Remove(comment));
            UnitOfWork.SaveChanges();
        }
    }
}
