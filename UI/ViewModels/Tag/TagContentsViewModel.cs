using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.ModelContainer;

namespace ZigmaWeb.UI.ViewModels.Tag
{
    public class TagContentsViewModel
    {
        public TagPM Tag { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public IEnumerable<ContentInfo3PM> Articles { get; set; }
        public int TotalRows { get; set; }
    }
}