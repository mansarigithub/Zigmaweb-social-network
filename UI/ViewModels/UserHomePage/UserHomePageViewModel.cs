using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.PresentationModel.ModelContainer;

namespace ZigmaWeb.UI.ViewModels.UserHomePage
{
    public class UserHomePageViewModel
    {
        public UserHomePageViewModel(UserInfoForHomePageModelContainer userHomePageModelContainer)
        {
            UserInfo = userHomePageModelContainer;
        }

        public UserInfoForHomePageModelContainer UserInfo { get; set; }
    }
}