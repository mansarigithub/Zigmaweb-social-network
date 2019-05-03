using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.PresentationModel.Model
{
    public class ProfileImageCropInfoPM
    {
        public float imgInitW { get; set; } // your image original width (the one we recieved after upload)
        public float imgInitH { get; set; } // your image original height (the one we recieved after upload)
        public float imgW { get; set; } // your new scaled image width
        public float imgH { get; set; } // your new scaled image height
        public float imgX1 { get; set; } // top left corner of the cropped image in relation to scaled image
        public float imgY1 { get; set; } // top left corner of the cropped image in relation to scaled image
        public float cropW { get; set; } // cropped image width
        public float cropH { get; set; } // cropped image height

        public float WidthZoomFactor
        {
            get
            {
                return imgW / imgInitW;
            }
        }

        public float HeightZoomFactor
        {
            get
            {
                return imgH / imgInitH;
            }
        }

        public Rectangle GetSourceImageCropArea()
        {
            return new Rectangle() {
                X = (int)(imgX1 / WidthZoomFactor),
                Y = (int)(imgY1 / HeightZoomFactor),
                Width = (int)(cropW / WidthZoomFactor),
                Height = (int)(cropH / HeightZoomFactor)
            };
        }
    }
}
