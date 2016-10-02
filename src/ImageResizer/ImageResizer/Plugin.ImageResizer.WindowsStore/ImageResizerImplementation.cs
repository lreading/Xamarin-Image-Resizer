using Plugin.ImageResizer.Abstractions;
using System;
using System.Threading.Tasks;

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
        public override Task<byte[]> ResizeImageWithAspectRatioAsync(byte[] sourceImage, int targetWidth, int targetHeight)
        {
            throw new NotImplementedException();
        }
    }
}