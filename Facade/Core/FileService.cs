using ZigmaWeb.Bussines.Core;
using ZigmaWeb.Security.Helper;
using System.Linq;
using System.Data.Entity;
using ZigmaWeb.Common.Data;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Common.ExtensionMethod;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.Security.Identity;
using ZigmaWeb.UnitOfWork;
using System;
using System.Collections;
using ZigmaWeb.PresentationModel.Model;
using System.Collections.Generic;
using ZigmaWeb.Mapper.Core;
using ZigmaWeb.Model.Domain.Core;
using System.IO;
using ZigmaWeb.Common.Configuration;
using Common.Exception;
using ZigmaWeb.Common.Drawing;
using System.Threading.Tasks;

namespace ZigmaWeb.Facade.Core
{
    public class FileService
    {
        CoreUnitOfWork UnitOfWork { get; set; }
        MediaBiz MediaBiz { get; set; }

        public FileService()
        {
            UnitOfWork = new CoreUnitOfWork();
            MediaBiz = new MediaBiz(UnitOfWork);
        }

        public MediaPM SaveMedia(Stream inputStream, int contentLength, string fileName, string contentType, int userId)
        {
            var extention = Path.GetExtension(fileName).ToLower();
            var mediaType = GetFileMediaType(extention);
            var userMediaDirectory = AppConfigurationManager.GetUserMediaDirectory();
            var newFileName = Guid.NewGuid().ToString();
            var newFileNameWithExt = $"{newFileName}{extention}";
            var filePath = $"{userMediaDirectory}\\{newFileName}{extention}";
            string thumbnailPath = null;

            try {
                using (FileStream fileStream = new FileStream(filePath, FileMode.CreateNew)) {
                    inputStream.CopyTo(fileStream);
                }

                if (mediaType == MediaType.Image) {
                    thumbnailPath = $"{userMediaDirectory}\\{newFileName}-thumb{extention}";
                    var task = CreateThumbnailImageAsync(inputStream, thumbnailPath);
                }

                var media = MediaBiz.AddMedia(newFileNameWithExt, contentLength, mediaType, userId);
                UnitOfWork.SaveChanges();
                return media.GetPresentationModel();
            }
            catch {
                File.Delete(filePath);
                if (thumbnailPath != null)
                    File.Delete(thumbnailPath);
                throw;
            }
        }

        private static async Task CreateThumbnailImageAsync(Stream inputStream, string thumbnailPath)
        {
            await Task.Factory.StartNew(() => {
                using (FileStream fileStream = new FileStream(thumbnailPath, FileMode.CreateNew)) {
                    inputStream.Seek(0, SeekOrigin.Begin);
                    var thumbnailBytes = ImageHelper.ResizeByteArrayImage(inputStream.ReadAllBytes(), 130, 130, false);
                    fileStream.Write(thumbnailBytes, 0, thumbnailBytes.Length);
                }
            });
        }

        public void DeleteUserMedia(int id, UserIdentity user)
        {
            try {
                var media = MediaBiz.ReadSingle(m => m.Id == id && m.UserId == user.UserId);
                File.Delete(AppFileManager.GetMediaFilePath(media.FileName));
                MediaBiz.Remove(media);
                UnitOfWork.SaveChanges();
            }
            catch {
                throw;
            }
        }

        public DataSourceResult ReadUserMediaList(DataSourceRequest request, UserIdentity user)
        {
            var result = MediaBiz
                .Read(media => media.UserId == user.UserId, noTracking: true)
                .MapTo<MediaPM>()
                .ToDataSourceResult(request);
            (result.data as List<MediaPM>).ForEach(media => {
                var ext = Path.GetExtension(media.FileName).ToLower();
                var fileName = Path.GetFileNameWithoutExtension(media.FileName);
                media.Url = $"http://zigmaweb.ir/usermedia/{media.FileName}";
                media.ThumbnailUrl = $"http://zigmaweb.ir/usermedia/{fileName}-thumb{ext}";
            });
            return result;
        }

        private MediaType GetFileMediaType(string e)
        {
            if (e == ".png" || e == ".jpeg" || e == ".jpg")
                return MediaType.Image;
            else if (e == ".mp4" || e == ".mpeg" || e == ".mpg")
                return MediaType.Video;
            else if (e == ".mp3")
                return MediaType.Sound;
            else
                throw new BusinessException("NotSupportedFileType");
        }
    }
}
