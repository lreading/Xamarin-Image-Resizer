using Microsoft.Phone;
using Plugin.ImageResizer.Abstractions;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Plugin.ImageResizer
{
    /// <summary>
    /// Implementation for ImageResizer
    /// </summary>
    public class ImageResizerImplementation : ImageResizerBase
    {

        /// <summary>
        /// Resizes an image with the target width/height while maintaining aspect ratio.
        /// </summary>
        /// <param name="sourceImage">The source image</param>
        /// <param name="targetWidth">The target width in pixels</param>
        /// <param name="targetHeight">The target height in pixels</param>
        /// <returns>byte[] of resized image</returns>
        public override async Task<byte[]> ResizeImageWithAspectRatioAsync(byte[] sourceImage, int targetWidth, int targetHeight)
        {
            return await Task.Run(() =>
            {
                using (var originalMs = new MemoryStream(sourceImage))
                {
                    var originalImage = PictureDecoder.DecodeJpeg(originalMs);
                    CalculateNewWidthAndHeight(originalImage.PixelWidth, targetWidth, originalImage.PixelHeight, targetHeight);
                }

                using (var newImgMs = new MemoryStream(sourceImage))
                {
                    var bitmap = PictureDecoder.DecodeJpeg(newImgMs, NewWidth, NewHeight);

                    using (var streamOut = new MemoryStream())
                    {
                        bitmap.SaveJpeg(streamOut, NewWidth, NewHeight, 0, 100);
                        return streamOut.ToArray();
                    }
                }
            });
        }
    }
}