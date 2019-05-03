using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using ZigmaWeb.Common.ExtensionMethod;

namespace ZigmaWeb.Common.Drawing
{
    public static class ImageHelper
    {
        public static Image GetThumbnailImage(this Image image, int width, int height)
        {
            return image.GetThumbnailImage(width, height, null, IntPtr.Zero);
        }

        public static void CropImage(string sourceImagePath, Rectangle sourceImageCropArea, Size targetImageSize, string targetImagePath, long saveQuality = 100L)
        {
            using (var sourceImage = new Bitmap(sourceImagePath)) {
                using (var cropedImage = sourceImage.Clone(sourceImageCropArea, sourceImage.PixelFormat)) {
                    using (var targetImage = new Bitmap(cropedImage, targetImageSize)) {
                        var imageFormat = GetImageFormat(sourceImage);
                        ImageCodecInfo jpgEncoder = GetEncoder(imageFormat);
                        var encoder = Encoder.Quality;
                        var encoderParameters = new EncoderParameters(1);
                        var encoderParameter = new EncoderParameter(encoder, saveQuality);
                        encoderParameters.Param[0] = encoderParameter;
                        targetImage.Save(targetImagePath, jpgEncoder, encoderParameters);
                    }
                }
            }
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs) {
                if (codec.FormatID == format.Guid) {
                    return codec;
                }
            }
            return null;
        }

        public static byte[] ResizeByteArrayImage(byte[] imageBytes, int newWidth, int newHeight)
        {
            var image = imageBytes.ToImage();
            return new Bitmap(image, newWidth, newHeight).ToByteArray(GetImageFormat(image));
        }

        public static byte[] ResizeByteArrayImage(byte[] imageBytes, int newHeight, int maxAllowedWidth = 1500, bool resizeIfImageHeightIsLowerThanNewHeight = true)
        {
            var image = imageBytes.ToImage();
            if (!resizeIfImageHeightIsLowerThanNewHeight && image.Height < newHeight)
                newHeight = image.Height ;
            var newWidth = newHeight * (image.Width / image.Height);
            newWidth = newWidth > maxAllowedWidth ? maxAllowedWidth : newWidth;
            return new Bitmap(image, newWidth, newHeight).ToByteArray(GetImageFormat(image));
        }

        public static ImageFormat GetImageFormat(Image image)
        {
            if (image.RawFormat.Guid.Equals(ImageFormat.Jpeg.Guid))
                return ImageFormat.Jpeg;
            else if (image.RawFormat.Guid.Equals(ImageFormat.Png.Guid))
                return ImageFormat.Png;
            else if (image.RawFormat.Guid.Equals(ImageFormat.Bmp.Guid))
                return ImageFormat.Bmp;
            else if (image.RawFormat.Guid.Equals(ImageFormat.Gif.Guid))
                return ImageFormat.Gif;
            else if (image.RawFormat.Guid.Equals(ImageFormat.Icon.Guid))
                return ImageFormat.Icon;
            else if (image.RawFormat.Guid.Equals(ImageFormat.Emf.Guid))
                return ImageFormat.Emf;
            else if (image.RawFormat.Guid.Equals(ImageFormat.Exif.Guid))
                return ImageFormat.Exif;

            return null;
        }

        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }

        public static string GetImageFormatFileExtension(ImageFormat imageFormat)
        {
            var guid = imageFormat.Guid;

            if (guid == ImageFormat.Jpeg.Guid)
                return ".jpeg";
            else if (guid == ImageFormat.Png.Guid)
                return ".png";
            else
                throw new NotSupportedException();
        }
    }
}
