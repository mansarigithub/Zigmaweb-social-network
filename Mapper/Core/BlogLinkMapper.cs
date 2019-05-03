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
    public static class BlogLinkMapper
    {
        public static void CreateMap(AutoMapperProfile profile)
        {
            profile.CreateMap<BlogLink, BlogLinkPM>();
            profile.CreateMap<BlogLinkPM, BlogLink>();
        }

        public static BlogLink GetLink(this BlogLinkPM linkPM)
        {
            return AutoMapper.Mapper.Map<BlogLinkPM, BlogLink>(linkPM);
        }

        public static BlogLinkPM GetLinkPM(this BlogLink link)
        {
            return AutoMapper.Mapper.Map<BlogLink, BlogLinkPM>(link);
        }
    }
}
