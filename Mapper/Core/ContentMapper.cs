using System.Collections.Generic;
using System.Linq;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Mapper.Attributes;
using ZigmaWeb.Mapper.Profile;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Content;

namespace ZigmaWeb.Mapper.Core
{
    [ObjectMapper]
    public static class ContentMapper
    {
        public static void CreateMap(AutoMapperProfile profile)
        {
            // ContentDetailsPM
            profile.CreateMap<Content, ContentRegistrationPM>()
                .ForMember(pm => pm.Published, opt => opt.MapFrom(model => model.State == ContentState.Published))
                .ForMember(pm => pm.AuthorFullName, opt => opt.MapFrom(model => model.Author.FullName));

            profile.CreateMap<Content, ContentForViewByVisitorPM>()
                .ForMember(pm => pm.Published, opt => opt.MapFrom(model => model.State == ContentState.Published))
                .ForMember(pm => pm.CreateDateString, opt => opt.Ignore())
                .ForMember(pm => pm.AuthorFullName, opt => opt.MapFrom(model => model.Author.FullName));

            profile.CreateMap<ContentRegistrationPM, Content>()
                .ForMember(model => model.State, opt => opt.MapFrom(pm => pm.Published ? ContentState.Published : ContentState.UnPublished));

            // ContentInfoPM
            profile.CreateMap<Content, ContentInfo1PM>()
                .ForMember(pm => pm.AuthorFirstName, opt => opt.MapFrom(model => model.Author.FirstName))
                .ForMember(pm => pm.AuthorLastName, opt => opt.MapFrom(model => model.Author.LastName))
                .ForMember(pm => pm.AuthorFullName, opt => opt.Ignore())
                .ForMember(pm => pm.CommentsCount, opt => opt.MapFrom(model => model.Comments.Count()))
                .ForMember(pm => pm.VisitsCount, opt => opt.MapFrom(model => model.Visits.Count()));

            // ContentInfoWithTextPM
            profile.CreateMap<Content, ContentInfoWithTextPM>()
                .ForMember(pm => pm.AuthorId, opt => opt.MapFrom(model => model.Author.Id))
                .ForMember(pm => pm.AuthorFirstName, opt => opt.MapFrom(model => model.Author.FirstName))
                .ForMember(pm => pm.AuthorLastName, opt => opt.MapFrom(model => model.Author.LastName))
                .ForMember(pm => pm.AuthorFullName, opt => opt.Ignore())
                .ForMember(pm => pm.Tags, opt => opt.MapFrom(model =>
                    model.Tags.Select(tag => new TagPM() { Id = tag.Id, Text = tag.Text })))
                .ForMember(pm => pm.CommentsCount, opt => opt.MapFrom(model => model.Comments.Count()));

            // ContentInfoForHomePage
            profile.CreateMap<Content, ContentInfo6PM>()
                .ForMember(pm => pm.AuthorFullName, opt => opt.Ignore())
                .ForMember(pm => pm.PublishDateString, opt => opt.Ignore())
                .ForMember(pm => pm.Tags, opt => opt.MapFrom(model =>
                    model.Tags.Select(tag => new TagPM() { Id = tag.Id, Text = tag.Text }).Take(6)))
                .ForMember(pm => pm.AuthorId, opt => opt.MapFrom(model => model.Author.Id))
                .ForMember(pm => pm.AuthorFirstName, opt => opt.MapFrom(model => model.Author.FirstName))
                .ForMember(pm => pm.AuthorLastName, opt => opt.MapFrom(model => model.Author.LastName));

            // ContentInfoForHomePage
            profile.CreateMap<Content, ContentInfo5PM>()
                .ForMember(pm => pm.PublishDateString, opt => opt.Ignore())
                .ForMember(pm => pm.CreateDateString, opt => opt.Ignore())
                .ForMember(pm => pm.Published, opt => opt.MapFrom(model => model.State == ContentState.Published))
                .ForMember(pm => pm.CommentsCount, opt => opt.MapFrom(model => model.Comments.Count()));

            // ContentInfoForGeneratingLink
            profile.CreateMap<Content, ContentInfo4PM>();

            // ContentInfoForGeneratingLink
            profile.CreateMap<Content, ContentInfo2PM>()
                .ForMember(pm => pm.CreateDateString, opt => opt.Ignore());

            // AdminDashboardNewArticlePm
            profile.CreateMap<Content, AdminDashboardNewArticlePm>()
                .ForMember(pm => pm.AuthorFirstName, opt => opt.MapFrom(model => model.Author.FirstName))
                .ForMember(pm => pm.AuthorLastName, opt => opt.MapFrom(model => model.Author.LastName))
                .ForMember(pm => pm.AuthorFullName, opt => opt.Ignore())
                .ForMember(pm => pm.PublishDateString, opt => opt.Ignore());

            profile.CreateMap<Content, ContentInfo3PM>()
                .ForMember(pm => pm.AuthorFullName, opt => opt.Ignore())
                .ForMember(pm => pm.CreateDateString, opt => opt.Ignore())
                .ForMember(pm => pm.AuthorFirstName, opt => opt.MapFrom(model => model.Author.FirstName))
                .ForMember(pm => pm.AuthorLastName, opt => opt.MapFrom(model => model.Author.LastName))
                .ForMember(pm => pm.Tags, opt => opt.MapFrom(model =>
                    model.Tags.Select(tag => new TagPM() { Id = tag.Id, Text = tag.Text })));
        }

        public static Content GetContent(this ContentRegistrationPM contentPresentationModel)
        {
            return AutoMapper.Mapper.Map<ContentRegistrationPM, Content>(contentPresentationModel);
        }

        public static ContentRegistrationPM GetContentRegistrationPM(this Content content)
        {
            return AutoMapper.Mapper.Map<Content, ContentRegistrationPM>(content);
        }

        public static ContentForViewByVisitorPM GetContentForViewByVisitorPM(this Content content)
        {
            return AutoMapper.Mapper.Map<Content, ContentForViewByVisitorPM>(content);
        }

        public static ContentInfoWithTextPM GetContentInfoWithTextPM(this Content content)
        {
            return AutoMapper.Mapper.Map<Content, ContentInfoWithTextPM>(content);
        }
    }
}
