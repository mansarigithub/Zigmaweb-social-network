using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZigmaWeb.PresentationModel.Model;

namespace ZigmaWeb.UI.Areas.User.ViewModels.Article
{
    public class EditArticleViewModel
    {
        public ContentRegistrationPM Content { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public int Length { get; set; }
    }
}