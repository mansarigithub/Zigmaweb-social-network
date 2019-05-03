using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using ZigmaWeb.DataAccess.Context;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Repository;

namespace ZigmaWeb.UnitOfWork
{
    public class CoreUnitOfWork : ICoreUnitOfWork
    {
        public ZigmaWebContext _context;

        private GenericRepository<User> _userRepository;
        public GenericRepository<User> UserRepository
        {
            get
            {
                return _userRepository == null ? _userRepository = new GenericRepository<User>(_context) : _userRepository;
            }
        }

        private GenericRepository<Role> _roleRepository;
        public GenericRepository<Role> RoleRepository
        {
            get
            {
                return _roleRepository == null ? _roleRepository = new GenericRepository<Role>(_context) : _roleRepository;
            }
        }

        private GenericRepository<Comment> _commentRepository;
        public GenericRepository<Comment> CommentRepository
        {
            get
            {
                return _commentRepository == null ? _commentRepository = new GenericRepository<Comment>(_context) : _commentRepository;
            }
        }

        private GenericRepository<Content> _contentRepository;
        public GenericRepository<Content> ContentRepository
        {
            get
            {
                return _contentRepository == null ? _contentRepository = new GenericRepository<Content>(_context) : _contentRepository;
            }
        }

        private GenericRepository<ForbiddenIdentifier> _forbiddenIdentifierRepository;
        public GenericRepository<ForbiddenIdentifier> ForbiddenIdentifierRepository
        {
            get
            {
                return _forbiddenIdentifierRepository == null ? _forbiddenIdentifierRepository = new GenericRepository<ForbiddenIdentifier>(_context) : _forbiddenIdentifierRepository;
            }
        }

        private GenericRepository<Reference> _referenceRepository;
        public GenericRepository<Reference> ReferenceRepository
        {
            get
            {
                return _referenceRepository == null ? _referenceRepository = new GenericRepository<Reference>(_context) : _referenceRepository;
            }
        }

        private GenericRepository<Tag> _tagRepository;
        public GenericRepository<Tag> TagRepository
        {
            get
            {
                return _tagRepository == null ? _tagRepository = new GenericRepository<Tag>(_context) : _tagRepository;
            }
        }

        private GenericRepository<Location> _locationRepository;
        public GenericRepository<Location> LocationRepository
        {
            get
            {
                return _locationRepository == null ? _locationRepository = new GenericRepository<Location>(_context) : _locationRepository;
            }
        }

        private GenericRepository<Rate> _rateRepository;
        public GenericRepository<Rate> RateRepository
        {
            get
            {
                return _rateRepository == null ? _rateRepository = new GenericRepository<Rate>(_context) : _rateRepository;
            }
        }

        private GenericRepository<Membership> _membershipRepository;
        public GenericRepository<Membership> MembershipRepository
        {
            get
            {
                return _rateRepository == null ? _membershipRepository = new GenericRepository<Membership>(_context) : _membershipRepository;
            }
        }

        private GenericRepository<Visit> _visitRepository;
        public GenericRepository<Visit> VisitRepository
        {
            get
            {
                return _visitRepository == null ? _visitRepository = new GenericRepository<Visit>(_context) : _visitRepository;
            }
        }

        private GenericRepository<ProfileKeyValue> _profileRepository;
        public GenericRepository<ProfileKeyValue> ProfileRepository
        {
            get
            {
                return _profileRepository == null ? _profileRepository = new GenericRepository<ProfileKeyValue>(_context) : _profileRepository;
            }
        }

        private GenericRepository<ExceptionLog> _exceptionLogRepository;
        public GenericRepository<ExceptionLog> ExceptionLogRepository
        {
            get
            {
                return _exceptionLogRepository == null ? _exceptionLogRepository = new GenericRepository<ExceptionLog>(_context) : _exceptionLogRepository;
            }
        }

        private GenericRepository<Publication> _publicationRepository;
        public GenericRepository<Publication> PublicationRepository
        {
            get
            {
                return _publicationRepository == null ? _publicationRepository = new GenericRepository<Publication>(_context) : _publicationRepository;
            }
        }

        private GenericRepository<Blog> _blogRepository;
        public GenericRepository<Blog> BlogRepository
        {
            get
            {
                return _blogRepository == null ? _blogRepository = new GenericRepository<Blog>(_context) : _blogRepository;
            }
        }

        private GenericRepository<Channel> _channelRepository;
        public GenericRepository<Channel> ChannelRepository
        {
            get
            {
                return _channelRepository == null ? _channelRepository = new GenericRepository<Channel>(_context) : _channelRepository;
            }
        }

        private GenericRepository<BlogLink> _friendRepository;
        public GenericRepository<BlogLink> FriendRepository
        {
            get
            {
                return _friendRepository == null ? _friendRepository = new GenericRepository<BlogLink>(_context) : _friendRepository;
            }
        }

        private GenericRepository<Notification> _notificationRepository;
        public GenericRepository<Notification> NotificationRepository
        {
            get
            {
                return _notificationRepository == null ? _notificationRepository = new GenericRepository<Notification>(_context) : _notificationRepository;
            }
        }

        private GenericRepository<Organization> _organizationRepository;
        public GenericRepository<Organization> OrganizationRepository
        {
            get
            {
                return _organizationRepository == null ? _organizationRepository = new GenericRepository<Organization>(_context) : _organizationRepository;
            }
        }

        private GenericRepository<UniversityField> _universityFieldRepository;
        public GenericRepository<UniversityField> UniversityFieldRepository
        {
            get
            {
                return _universityFieldRepository == null ? _universityFieldRepository = new GenericRepository<UniversityField>(_context) : _universityFieldRepository;
            }
        }

        private GenericRepository<EducationalResume> _educationalResumeRepository;
        public GenericRepository<EducationalResume> EducationalResumeRepository
        {
            get
            {
                return _educationalResumeRepository == null ? _educationalResumeRepository = new GenericRepository<EducationalResume>(_context) : _educationalResumeRepository;
            }
        }

        private GenericRepository<Job> _jobRepository;
        public GenericRepository<Job> JobRepository
        {
            get
            {
                return _jobRepository == null ? _jobRepository = new GenericRepository<Job>(_context) : _jobRepository;
            }
        }

        private GenericRepository<JobResume> _jobResumeRepository;
        public GenericRepository<JobResume> JobResumeRepository
        {
            get
            {
                return _jobResumeRepository == null ? _jobResumeRepository = new GenericRepository<JobResume>(_context) : _jobResumeRepository;
            }
        }

        private GenericRepository<Media> _mediaRepository;
        public GenericRepository<Media> MediaRepository
        {
            get
            {
                return _mediaRepository == null ? _mediaRepository = new GenericRepository<Media>(_context) : _mediaRepository;
            }
        }

        private GenericRepository<Follow> _followRepository;
        public GenericRepository<Follow> FollowRepository
        {
            get
            {
                return _followRepository == null ? _followRepository = new GenericRepository<Follow>(_context) : _followRepository;
            }
        }

        private GenericRepository<Message> _messageRepository;
        public GenericRepository<Message> MessageRepository
        {
            get
            {
                return _messageRepository == null ? _messageRepository = new GenericRepository<Message>(_context) : _messageRepository;
            }
        }

        private GenericRepository<FeaturedContent> _featuredContentRepository;
        public GenericRepository<FeaturedContent> FeaturedContentRepository
        {
            get
            {
                return _featuredContentRepository == null ? _featuredContentRepository = new GenericRepository<FeaturedContent>(_context) : _featuredContentRepository;
            }
        }

        public CoreUnitOfWork()
        {
            _context = new ZigmaWebContext();
        }

        public GenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new()
        {
            return (GenericRepository<TEntity>)this.GetType()
                .GetProperties()
                .Single(propertyInfo => propertyInfo.PropertyType == typeof(GenericRepository<TEntity>))
                .GetValue(this);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync(bool clearContextCache = true)
        {
            return _context.SaveChangesAsync();
        }
    }
}
