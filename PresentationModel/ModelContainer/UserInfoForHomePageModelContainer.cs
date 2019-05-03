using System.Collections.Generic;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.PresentationModel.Model.JobResume;
using ZigmaWeb.PresentationModel.Model.Organization;

namespace ZigmaWeb.PresentationModel.ModelContainer
{
    public class UserInfoForHomePageModelContainer
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AboutMe { get; set; }
        public string FacebookUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string WebSiteUrl { get; set; }
        public int TotalArticles { get; set; }
        public int TotalBlogPosts { get; set; }
        public int TotalVisits { get; set; }
        public string WeblogName { get; set; }
        public string RegistrationDateString { get; set; }
        public IEnumerable<ContentInfo2PM> LatestArticles { get; set; }
        public IEnumerable<ContentInfo2PM> LatestBlogPosts { get; set; }
        public IEnumerable<EducationalResumePM> EducationalResumes { get; set; }
        public IEnumerable<JobResumePM> JobResumes { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public bool CurrentUserFollowsThisUser { get; set; }
        public int Followers { get; set; }
    }
}
