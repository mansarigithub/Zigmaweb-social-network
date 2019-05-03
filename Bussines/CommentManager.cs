using System.Linq;
using Common.Web;
using ZigmaWeb.Model.Entity.ZwCore;
using ZigmaWeb.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ZigmaWeb.Bussines
{
    public class CommentManager : BaseManager
    {
        #region Implementing Singleton
        static CommentManager _instance;

        private CommentManager()
        {
        }

        public static CommentManager Instance
        {
            get
            {
                return (_instance ?? (_instance = new CommentManager()));
            }
        }
        #endregion

        public Comment AddComment(Comment comment)
        {
            return Repository.Add(comment);
        }

        public IEnumerable<Comment> GetComment(Expression<Func<Comment, bool>> predicate)
        {
            return Repository.AsQueryable<Comment>().Where(predicate).AsEnumerable();
        }

        public Comment GetComment(int commentId)
        {
            return GetComment(e => e.Id == commentId).First();
        }


        public void DeleteComment(int commentId)
        {
            var comment = Repository.AsQueryable<Comment>("Content").First(e => e.Id == commentId);
            Repository.Remove<Comment>(comment);
            Repository.SaveChanges();
        }
        public void DeleteComment(Comment comment)
        {
            Repository.Remove<Comment>(comment);
            Repository.SaveChanges();
        }

    }
}