using Android.Graphics;
using Plugin.ImageResizer.Abstractions;
using System.IO;
using System.Threading.Tasks;

namespace Plugin.ImageResizer
{
    /// <summary>
    /// Implementation for Feature
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
                using (var originalImage = BitmapFactory.DecodeByteArray(sourceImage, 0, sourceImage.Length))
                {
                    CalculateNewWidthAndHeight(originalImage.Width, targetWidth, originalImage.Height, targetHeight);

                    using (var resizedImage = Bitmap.CreateScaledBitmap(originalImage, NewWidth, NewHeight, false))
                    {
                        using (var ms = new MemoryStream())
                        {
                            resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                            return ms.ToArray();
                        }
                    }
                }
            });
        }
    }
}