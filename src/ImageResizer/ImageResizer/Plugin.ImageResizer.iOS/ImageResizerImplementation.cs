using CoreGraphics;
using Plugin.ImageResizer.Abstractions;
using System;
using System.Drawing;
using System.Threading.Tasks;
using UIKit;
using System.IO;

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
                using (var originalImage = ImageFromByteArray(sourceImage))
                {
                    var orientation = originalImage.Orientation;

                    CalculateNewWidthAndHeight((int)originalImage.Size.Width, targetWidth, (int)originalImage.Size.Height, targetHeight);

                    //create a 24bit RGB image
                    using (var context = new CGBitmapContext(IntPtr.Zero, NewWidth, NewHeight, 8, (4 * NewWidth),
                        CGColorSpace.CreateDeviceRGB(), CGImageAlphaInfo.PremultipliedFirst))
                    {
                        var imageRect = new RectangleF(0, 0, NewWidth, NewHeight);

                        // draw the image
                        context.DrawImage(imageRect, originalImage.CGImage);

                        var resizedImage = UIImage.FromImage(context.ToImage(), 0, orientation);

                        // save the image as a jpeg
                        return resizedImage.AsJPEG().ToArray();
                    }
                }
            });
        }

        private UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null) return null;
            return new UIImage(Foundation.NSData.FromArray(data));
        }
    }
}