using System;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using ZigmaWeb.Common.Enum;
using System.Linq;
using System.Data.Entity;

namespace ZigmaWeb.Bussines.Core
{
    public class CommentBiz : BizBase<Comment>
    {
        public CommentBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {

        }

        public void AddComment(Comment comment)
        {
            comment.CreateDate = DateTime.Now;
            comment.Status = CommentStatus.NotConfirmed;
            UnitOfWork.CommentRepository.Add(comment);
        }

        public IQueryable<Comment> ReadNotConfirmedComments(int userId)
        {
            return Read(comment => comment.Content.AuthorId == userId && comment.Status == CommentStatus.NotConfirmed);
        }

        public IQueryable<Comment> GetArticleComments(int articleId)
        {
            return Read(comment => comment.Content.Id == articleId && !comment.IsPrivate)
                .Include(comment => comment.Sender);
        }
    }
}