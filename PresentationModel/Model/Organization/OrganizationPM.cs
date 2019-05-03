using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.PresentationModel.Model.Organization
{
    public class OrganizationPM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OrganizationType Type { get; set; }
    }
}
