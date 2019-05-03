namespace ZigmaWeb.Model.Domain.Core
{
    public partial class FeaturedContent
    {
        public FeaturedContent()
        {
        }

        public int Id { get; set; }
        public int ContentId { get; set; }
        public virtual Content Content { get; set; }
    }
}
