using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace HSNXT.ComLib.MediaSupport
{
    /// <summary>
    /// Helper class for graphics processing.
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// Convert byte arry to an image object.
        /// </summary>
        /// <param name="content">The bytes representing the image.</param>
        /// <returns>Image created from bytes.</returns>
        public static Image ConvertToImage(byte[] content)
        {
            var ms = new MemoryStream(content);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }


        /// <summary>
        /// Converts an image to a byte array.
        /// </summary>
        /// <param name="image">Image to convert.</param>
        /// <returns>Array of bytes with converted image.</returns>
        public byte[] ConvertToBytes(Image image)
        {            
            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Gif);
            return ms.ToArray();
        }


        /// <summary>
        /// Converts an image to a byte array.
        /// </summary>
        /// <param name="image">Image to convert.</param>
        /// <param name="format">The format of the image.</param>
        /// <returns>Array of bytes with converted image.</returns>
        public byte[] ConvertToBytes(Image image, ImageFormat format)
        {
            var ms = new MemoryStream();
            image.Save(ms, format);
            return ms.ToArray();
        }


        /// <summary>
        /// Convert the image contents as a byte[] into a thumbnail represented by another byte[].
        /// </summary>
        /// <param name="imageContents">The byte[] for the original contents.</param>
        /// <param name="thumbNailHeigth">Height for thumbnail</param>
        /// <param name="thumbNailWidth">Width for thumbnail</param>
        /// <returns>Processing results.</returns>
        public static BoolMessageItem<byte[]> ConvertToThumbNail(byte[] imageContents, int thumbNailWidth, int thumbNailHeigth)
        {
            byte[] thumbnailBytes = null;
            BoolMessageItem<byte[]> result = null;
            
            if (thumbNailWidth == 0) thumbNailWidth = 50;
            if (thumbNailHeigth == 0) thumbNailHeigth = 40;

            try
            {   
                using (var image = ConvertToImage(imageContents))
                {
                    // Now generate the thumbnail.
                    using (var thumbnail = image.GetThumbnailImage(50, 40, null, new IntPtr()))
                    {
                        // The below code converts an Image object to a byte array
                        using (var ms = new MemoryStream())
                        {
                            thumbnail.Save(ms, ImageFormat.Jpeg);
                            thumbnailBytes = ms.ToArray();
                            result = new BoolMessageItem<byte[]>(thumbnailBytes, true, string.Empty);
                        }
                    }
                }
            }
            catch (Exception)
            {
                result = new BoolMessageItem<byte[]>(null, false, "Error converting to thumbnail.");
            }
            return result;
        }


        /// <summary>
        /// Returns the height and width of the image.
        /// </summary>
        /// <param name="imageContents">Byte array with converted image.</param>
        /// <returns>Tuple with height and width.</returns>
        public static Tuple2<int, int> GetDimensions(byte[] imageContents)
        {
            var image = ConvertToImage(imageContents);
            var width = Convert.ToInt32(image.PhysicalDimension.Width);
            var height = Convert.ToInt32(image.PhysicalDimension.Height);
            return new Tuple2<int, int>(height, width);
        }
    }
}
