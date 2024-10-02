using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Data.Linq;

namespace FruitPowerAPI
{
    public class ImageConverter
    {
        public static Binary ConvertImageToBinary(string dataUrl)
        {
            var base64Data = dataUrl.Substring(dataUrl.IndexOf(",") + 1);
            byte[] imageBytes = Convert.FromBase64String(base64Data);
            return new Binary(imageBytes);
        }

        public static Image ConvertBinaryToImage(Binary binary)
        {
            byte[] imageBytes = binary.ToArray();
            using (var ms = new MemoryStream(imageBytes))
            {
                return Image.FromStream(ms);
            }
        }

    }
}