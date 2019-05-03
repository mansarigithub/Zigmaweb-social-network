using System;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.PresentationModel.Model
{
    public class MediaPM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public int Size { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
        public MediaType Type { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
