using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.UI.Infrastructure.StaticResource
{
    public static class StaticResourceHelper
    {
        public static string GetAnonymousUserIconPhysicalPath(ProfileImageSize profileImageSize)
        {
            string fileName;
            switch (profileImageSize) {
                case ProfileImageSize.Large:
                    fileName = "user_300x300.png";
                    break;
                case ProfileImageSize.Medium:
                    fileName = "user_80x80.png";
                    break;
                case ProfileImageSize.Small:
                    fileName = "user_40x40.png";
                    break;
                default:
                    fileName = "user_40x40.png";
                    break;
            }
            return HttpContext.Current.Server.MapPath($"~/content/assets/global/img/user/{fileName}");
        }
    }
}