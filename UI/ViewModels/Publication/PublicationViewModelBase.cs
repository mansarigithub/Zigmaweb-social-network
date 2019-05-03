namespace ZigmaWeb.UI.ViewModels.Publication
{
    public class PublicationViewModelBase 
    {
        public int PublicationId { get; set; }
        public int CreatorId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }

        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }
        public string CreatorFullName
        {
            get
            {
                return $"{CreatorFirstName} {CreatorLastName}";
            }
        }
    }
}