using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using ZigmaWeb.Bussines.Core;
using ZigmaWeb.Common.Data;
using ZigmaWeb.Common.DataProvider;
using ZigmaWeb.Common.ExtensionMethod;
using ZigmaWeb.Common.Globalization;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Mapper.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.PresentationModel.ModelContainer;
using ZigmaWeb.Security.Identity;
using ZigmaWeb.UnitOfWork;
using ZigmaWeb.PresentationModel.Model.SetupBlog;
using ZigmaWeb.PresentationModel.Model.Blog;

namespace ZigmaWeb.Facade.Core
{
    public class SetupBlogService
    {
        CoreUnitOfWork UnitOfWork { get; set; }
        BlogBiz BlogBiz { get; set; }
        TagBiz TagBiz { get; set; }

        public SetupBlogService()
        {
            UnitOfWork = new CoreUnitOfWork();
            BlogBiz = new BlogBiz(UnitOfWork);
            TagBiz = new TagBiz(UnitOfWork);
        }

        public BlogGeneralSettingsPM ReadBlogGeneralSettings(int blogId)
        {
            return BlogBiz.ReadSingle(b => b.Id == blogId).GetGeneralSettingsPM();
        }

        public void UpdateBlogGeneralSettings(int blogId, BlogGeneralSettingsPM generalSettings)
        {
            var blog = BlogBiz.ReadSingle(b => b.Id == blogId);
            var updatedBlog = generalSettings.GetBlog();
            blog.Title = updatedBlog.Title;
            blog.Description = updatedBlog.Description;
            blog.Email = updatedBlog.Email;
            UnitOfWork.SaveChanges();
        }

        public int CreateBlog(UserIdentity UserIdentity, BlogGeneralSettingsPM blogRegisterationPM)
        {
            var blog = blogRegisterationPM.GetBlog();
            blog.CreatorId = UserIdentity.UserId;
            BlogBiz.CreateBlog(blog);
            UnitOfWork.SaveChanges();

            UserIdentity.HasBlog = true;
            UserIdentity.Blogs.Add(new ShortBlogInfoPM() {
                Id = blog.Id,
                Name = blog.Name,
                Title = blog.Title
            });
            return blog.Id;
        }
    }
}
