using System;
using System.IO;
using System.Threading.Tasks;

namespace Plugin.ImageResizer.Abstractions
{
    /// <summary>
    /// Base class for resizing images on individual platforms.
    /// </summary>
    public abstract class ImageResizerBase : IImageResizer
    {
        /// <summary>
        /// The new height in pixels to be used for resizing the image
        /// </summary>
        public int NewHeight { get; private set; }

        /// <summary>
        /// The new width in pixels to be used for resizing the image
        /// </summary>
        public int NewWidth { get; private set; }

        /// <summary>
        /// The ratio used to calculate the new width/height
        /// </summary>
        public double Ratio { get; private set; }

        /// <summary>
        /// Resizes an image with the target width/height while maintaining aspect ratio.
        /// </summary>
        /// <param name="sourceImage">The source image</param>
        /// <param name="targetWidth">The target width in pixels</param>
        /// <param name="targetHeight">The target height in pixels</param>
        /// <returns>byte[] of resized image</returns>
        public abstract Task<byte[]> ResizeImageWithAspectRatioAsync(byte[] sourceImage, int targetWidth, int targetHeight);

        /// <summary>
        /// Resizes an image with the target width/height while maintaining aspect ratio.
        /// </summary>
        /// <param name="sourceImage">The source image</param>
        /// <param name="targetWidth">The target width in pixels</param>
        /// <param name="targetHeight">The target height in pixels</param>
        /// <returns>byte[] of resized image</returns>
        public async Task<byte[]> ResizeImageWithAspectRatioAsync(Stream sourceImage, int targetWidth, int targetHeight)
        {
            var originalBytes = ReadBytesFully(sourceImage);
            return await ResizeImageWithAspectRatioAsync(originalBytes, targetWidth, targetHeight);
        }

        /// <summary>
        /// Reads all bytes from a stream.
        /// </summary>
        /// <remarks>
        /// Borrowed from Jon Skeet's solution on Stack Overflow: http://stackoverflow.com/a/221941/3033053
        /// </remarks>
        /// <param name="input"></param>
        /// <returns>All bytes from the stream</returns>
        public byte[] ReadBytesFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        
        /// <summary>
        /// Calculates the new widths and heights to use based on the existing
        /// widths/heights and the target widths/heights
        /// </summary>
        /// <remarks>
        /// Populates Ratio, NewHeight and NewWidth properties
        /// </remarks>
        /// <param name="originalWidth"></param>
        /// <param name="targetWidth"></param>
        /// <param name="originalHeight"></param>
        /// <param name="targetHeight"></param>
        public void CalculateNewWidthAndHeight(int originalWidth, int targetWidth, int originalHeight, int targetHeight)
        {
            // Determine the ratio
            var ratioX = (double)targetWidth / originalWidth;
            var ratioY = (double)targetHeight / originalHeight;
            Ratio = Math.Min(ratioX, ratioY);

            // Calculate the new width/height
            NewWidth = (int)(originalWidth * Ratio);
            NewHeight = (int)(originalHeight * Ratio);
        }
    }
}
