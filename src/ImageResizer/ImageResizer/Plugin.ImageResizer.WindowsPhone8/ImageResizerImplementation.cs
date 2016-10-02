using Microsoft.Phone;
using Plugin.ImageResizer.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Plugin.ImageResizer
{
    /// <summary>
    /// Implementation for ImageResizer
    /// </summary>
    public class ImageResizerImplementation : IImageResizer
    {
        public async Task<byte[]> ResizeImageWithAspectRatioAsync(byte[] sourceImage, int maxWidth, int maxHeight)
        {
            return await Task.Run(() =>
            {
                int newWidth;
                int newHeight;

                using (var originalMs = new MemoryStream(sourceImage))
                {
                    var originalImage = PictureDecoder.DecodeJpeg(originalMs);

                    var ratioX = (double)maxWidth / originalImage.PixelWidth;
                    var ratioY = (double)maxHeight / originalImage.PixelHeight;
                    var ratio = Math.Min(ratioX, ratioY);

                    newWidth = (int)(originalImage.PixelWidth * ratio);
                    newHeight = (int)(originalImage.PixelHeight * ratio);
                }

                using (var newImgMs = new MemoryStream(sourceImage))
                {
                    var bitmap = PictureDecoder.DecodeJpeg(newImgMs, newWidth, newHeight);

                    using (var streamOut = new MemoryStream())
                    {
                        bitmap.SaveJpeg(streamOut, newWidth, newHeight, 0, 100);
                        return streamOut.ToArray();
                    }
                }
            });
        }
    }
}