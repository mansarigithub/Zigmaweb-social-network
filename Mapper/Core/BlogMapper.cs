using System.Collections.Generic;
using System.Linq;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Mapper.Attributes;
using ZigmaWeb.Mapper.Profile;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Blog;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.PresentationModel.Model.SetupBlog;

namespace ZigmaWeb.Mapper.Core
{
    [ObjectMapper]
    public static class BlogMapper
    {
        public static void CreateMap(AutoMapperProfile profile)
        {
            profile.CreateMap<BlogGeneralSettingsPM, Blog>();
            profile.CreateMap<Blog, BlogGeneralSettingsPM>();

            profile.CreateMap<Blog, ShortBlogInfoPM>();
            profile.CreateMap<Blog, BlogInfoPM>()
                .ForMember(pm => pm.CreatorFullName, opt => opt.Ignore());

            profile.CreateMap<Blog, BlogInfoForGenerateLinkPM>();
        }

        public static Blog GetBlog(this BlogGeneralSettingsPM blogRegisterationPM)
        {
            return AutoMapper.Mapper.Map<BlogGeneralSettingsPM, Blog>(blogRegisterationPM);
        }

        public static BlogGeneralSettingsPM GetGeneralSettingsPM(this Blog blog)
        {
            return AutoMapper.Mapper.Map<Blog, BlogGeneralSettingsPM>(blog);
        }

        public static BlogInfoPM GetBlogInfo(this Blog blog)
        {
            return AutoMapper.Mapper.Map<Blog, BlogInfoPM>(blog);
        }

        public static ShortBlogInfoPM GetShortBlogInfo(this Blog blog)
        {
            return AutoMapper.Mapper.Map<Blog, ShortBlogInfoPM>(blog);
        }
        public static BlogInfoForGenerateLinkPM GetBlogInfoForGenerateLink(this Blog blog)
        {
            return AutoMapper.Mapper.Map<Blog, BlogInfoForGenerateLinkPM>(blog);
        }
    }
}
