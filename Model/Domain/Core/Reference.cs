namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Reference
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public int CultureLcid { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public virtual Content Content { get; set; }
    }
}
