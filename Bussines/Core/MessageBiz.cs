using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using ZigmaWeb.PresentationModel.Model;

namespace ZigmaWeb.Bussines.Core
{
    public class MessageBiz : BizBase<Message>
    {
        public MessageBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }

        public IQueryable<Message> ReadUserMessages(int userId, int count)
        {
            return Read(m => m.ReceiverId == userId)
                .OrderByDescending(m => m.CreateDate)
                .Take(count);
        }

        public Message Add(int senderId, int receiverId, string text)
        {
            return Add(new Message() {
                CreateDate = DateTime.Now,
                SenderId = senderId,
                ReceiverId = receiverId,
                Text = text
            });
        }
    }
}