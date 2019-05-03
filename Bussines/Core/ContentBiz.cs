using System;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using ZigmaWeb.Common.Enum;
using System.Linq;
using System.Collections.Generic;
using ZigmaWeb.Common.ExtensionMethod;
using ZigmaWeb.Common.Globalization;
using System.Data.Entity;

namespace ZigmaWeb.Bussines.Core
{
    public class ContentBiz : BizBase<Content>
    {
        public ContentBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }

        public IQueryable<Content> ReadUserPublishedContents(int userId, ContentType contentType)
        {
            return ReadUserContents(userId, contentType).Where(content => content.State == ContentState.Published);
        }

        public IQueryable<Content> ReadUserContents(int userId, ContentType contentType)
        {
            return UnitOfWork.ContentRepository
                .Read(content =>
                    content.AuthorId == userId &&
                    content.Type == contentType);
        }


        public void AddContent(Content content, IEnumerable<string> tags)
        {
            List<Tag> newTags = new List<Tag>();
            var contentHasTags = tags != null && tags.Count() > 0;
            content.CreateDate = DateTime.Now;
            content.PersianCreateDateYear = (short)DateTime.Now.GetPersianYear();
            content.PersianCreateDateMonth = (byte)DateTime.Now.GetPersianMonth();
            content.PublishDate = content.State == ContentState.Published ? DateTime.Now : (DateTime?)null;

            if (contentHasTags) {
                var currentlyExistTags = UnitOfWork.TagRepository.Read(t => tags.Contains(t.Text)).ToList();
                tags.ForEach(tag => {
                    if (!currentlyExistTags.Any(t => t.Text == tag)) {
                        newTags.Add(new Tag() {
                            Text = tag.Trim()
                        });
                    }
                });
                content.Tags.AddRange(currentlyExistTags);
                content.Tags.AddRange(newTags);
            }

            UnitOfWork.ContentRepository.Add(content);
        }

        public void UpdateContent(Content content, IEnumerable<string> tags)
        {
            List<Tag> newTags = new List<Tag>();
            var contentHasTags = tags != null && tags.Count() > 0;
            var fetchedContent = Read(c => c.Id == content.Id).Include(c => c.Tags).Single();
            fetchedContent.Title = content.Title;
            fetchedContent.Text = content.Text;
            fetchedContent.State = content.State;
            fetchedContent.ShortAbstract = content.ShortAbstract;
            fetchedContent.PublishDate = fetchedContent.PublishDate ??
                (content.State == ContentState.Published ? DateTime.Now : (DateTime?)null);
            fetchedContent.Tags.ToList().ForEach(tag => fetchedContent.Tags.Remove(tag));

            if (contentHasTags) {
                var currentlyExistTags = UnitOfWork.TagRepository.Read(t => tags.Contains(t.Text)).ToList();
                tags.ForEach(tag => {
                    if (!currentlyExistTags.Any(t => t.Text == tag)) {
                        newTags.Add(new Tag() {
                            Text = tag.Trim()
                        });
                    }
                });
                fetchedContent.Tags.AddRange(newTags);
                fetchedContent.Tags.AddRange(currentlyExistTags);
            }
        }

    }
}