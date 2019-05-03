using System;
using System.Data.Entity;
using System.Diagnostics;
using ZigmaWeb.DataAccess.Mapping.Core;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Context
{
    public class ZigmaWebContext : DbContext
    {
        public ZigmaWebContext() : base("Name=ZigmaWebConnectionString")
        {
            Database.SetInitializer<ZigmaWebContext>(null);
            this.Configuration.LazyLoadingEnabled = false;
            EnableLog();
        }

        private void EnableLog()
        {
            this.Database.Log = msg => Debug.WriteLine($"SQL: {msg}");
        }

        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ForbiddenIdentifier> ForbiddenIdentifiers { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProfileKeyValue> ProfileKeyValues { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<UniversityField> UniversityFields { get; set; }
        public DbSet<EducationalResume> EducationalResumes { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobResume> JobResumes { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<FeaturedContent> FeaturedContents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MembershipMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new ContentMap());
            modelBuilder.Configurations.Add(new ForbiddenIdentifierMap());
            modelBuilder.Configurations.Add(new ReferenceMap());
            modelBuilder.Configurations.Add(new VisitMap());
            modelBuilder.Configurations.Add(new TagMap());
            modelBuilder.Configurations.Add(new ProfileKeyValueMap());
            modelBuilder.Configurations.Add(new LocationMap());
            modelBuilder.Configurations.Add(new RateMap());
            modelBuilder.Configurations.Add(new ExceptionLogMap());
            modelBuilder.Configurations.Add(new PublicationMap());
            modelBuilder.Configurations.Add(new BlogMap());
            modelBuilder.Configurations.Add(new ChannelMap());
            modelBuilder.Configurations.Add(new BlogLinkMap());
            modelBuilder.Configurations.Add(new NotificationMap());
            modelBuilder.Configurations.Add(new UniversityFieldMap());
            modelBuilder.Configurations.Add(new OrganizationMap());
            modelBuilder.Configurations.Add(new EducationalResumeMap());
            modelBuilder.Configurations.Add(new JobMap());
            modelBuilder.Configurations.Add(new JobResumeMap());
            modelBuilder.Configurations.Add(new MediaMap());
            modelBuilder.Configurations.Add(new FollowMap());
            modelBuilder.Configurations.Add(new MessageMap());
            modelBuilder.Configurations.Add(new FeaturedContentMap());
        }
    }
}
