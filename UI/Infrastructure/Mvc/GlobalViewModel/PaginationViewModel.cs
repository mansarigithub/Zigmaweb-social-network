namespace ZigmaWeb.UI.Infrastructure.Mvc.GlobalViewModel
{
    public class PaginationViewModel
    {
        public int TotalRows { get; set; }
        public int PageSize { get; set; }
        public int SelectedPage { get; set; }
        public int VisiblePreviousPages { get; set; }
        public int VisibleNextPages { get; set; }
        public string UrlTemplate { get; set; }
    }
}