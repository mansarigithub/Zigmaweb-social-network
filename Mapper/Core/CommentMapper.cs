using ZigmaWeb.Common.Enum;
using ZigmaWeb.Mapper.Attributes;
using ZigmaWeb.Mapper.Profile;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.Common.Globalization;

namespace ZigmaWeb.Mapper.Core
{
    [ObjectMapper]
    public static class CommentMapper
    {
        public static void CreateMap(AutoMapperProfile profile)
        {
            profile.CreateMap<Comment, CommentInfoPM>()
                  .ForMember(pm => pm.SenderFullName, opt => opt.Ignore())
                  .ForMember(pm => pm.CreateDateString, opt => opt.Ignore())
                  .ForMember(pm => pm.ContentTitle, opt => opt.MapFrom(model => model.Content.Title));
            profile.CreateMap<CommentInfoPM, Comment>();

            profile.CreateMap<CommentRegistrationPM, Comment>();
        }

        public static Comment GetComment(this CommentInfoPM commentPresentationModel)
        {
            return AutoMapper.Mapper.Map<CommentInfoPM, Comment>(commentPresentationModel);
        }

        public static CommentInfoPM  GetCommentInfoPM(this Comment comment)
        {
            return AutoMapper.Mapper.Map<Comment, CommentInfoPM>(comment);
        }

        public static Comment GetComment(this CommentRegistrationPM commentRegistrationPM)
        {
            return AutoMapper.Mapper.Map<CommentRegistrationPM, Comment>(commentRegistrationPM);
        }

        public static CommentRegistrationPM GetCommentRegistrationPM(this Comment comment)
        {
            return AutoMapper.Mapper.Map<Comment, CommentRegistrationPM>(comment);
        }
    }
}
