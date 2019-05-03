using ZigmaWeb.Bussines.Core;
using System.Linq;
using System.Data.Entity;
using ZigmaWeb.Security.Identity;
using ZigmaWeb.UnitOfWork;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.Mapper.Core;
using ZigmaWeb.Model.Domain.Core;
using System.Collections.Generic;
using ZigmaWeb.Common.Enum;
using System.IO;
using Common.Exception;
using ZigmaWeb.Localization.ExtensionMethod;
using System.Drawing;
using ZigmaWeb.Common.Configuration;
using ZigmaWeb.Common.Drawing;
using System.Drawing.Imaging;
using ZigmaWeb.Common.IO;
using ZigmaWeb.PresentationModel.ModelContainer;
using ZigmaWeb.Common.ExtensionMethod;
using ZigmaWeb.PresentationModel.Model.Organization;
using ZigmaWeb.Common.Globalization;
using System;
using ZigmaWeb.PresentationModel.Model.Business;

namespace ZigmaWeb.Facade.Core
{
    public class UserBusinessService
    {
        CoreUnitOfWork UnitOfWork { get; set; }
        UserBiz UserBiz { get; set; }
        MembershipBiz MembershipBiz { get; set; }
        ProfileBiz ProfileBiz { get; set; }
        ArticleBiz ArticleBiz { get; set; }
        BlogBiz BlogBiz { get; set; }
        VisitBiz VisitBiz { get; set; }
        JobResumeBiz JobResumeBiz { get; set; }
        EducationalResumeBiz EducationalResumeBiz { get; set; }
        FollowBiz FollowBiz { get; set; }

        public UserBusinessService()
        {
            UnitOfWork = new CoreUnitOfWork();
            UserBiz = new UserBiz(UnitOfWork);
            MembershipBiz = new MembershipBiz(UnitOfWork);
            ProfileBiz = new ProfileBiz(UnitOfWork);
            ArticleBiz = new ArticleBiz(UnitOfWork);
            BlogBiz = new BlogBiz(UnitOfWork);
            EducationalResumeBiz = new EducationalResumeBiz(UnitOfWork);
            VisitBiz = new VisitBiz(UnitOfWork);
            JobResumeBiz = new JobResumeBiz(UnitOfWork);
            FollowBiz = new FollowBiz(UnitOfWork);
        }

        #region General Settings
        public BusinessIntroducePM ReadUserBusinesIntroduce(int userId)
        {
            string text;
            return new BusinessIntroducePM() {
                Text = ProfileBiz.TryReadUserProfileValue(userId, ProfileKeyValueType.UserBusinessIntroduceText, out text) ?
                    text : ""
            };
        }

        public void UpdateBusinesIntroduce(UserIdentity userIdentity, BusinessIntroducePM businessIntroduce)
        {
            ProfileBiz.SetUserProfile(userIdentity.UserId, ProfileKeyValueType.UserBusinessIntroduceText, businessIntroduce.Text);
            UnitOfWork.SaveChanges();
        }
        #endregion
    }
}
