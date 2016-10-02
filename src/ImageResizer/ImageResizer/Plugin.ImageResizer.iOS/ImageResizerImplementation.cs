using CoreGraphics;
using Plugin.ImageResizer.Abstractions;
using System;
using System.Drawing;
using System.Threading.Tasks;
using UIKit;

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
                using (var originalImage = ImageFromByteArray(sourceImage))
                {
                    var orientation = originalImage.Orientation;

                    var ratioX = (double)maxWidth / originalImage.Size.Width;
                    var ratioY = (double)maxHeight / originalImage.Size.Height;
                    var ratio = Math.Min(ratioX, ratioY);

                    var newWidth = (int)(originalImage.Size.Width * ratio);
                    var newHeight = (int)(originalImage.Size.Height * ratio);

                    //create a 24bit RGB image
                    using (var context = new CGBitmapContext(IntPtr.Zero, newWidth, newHeight, 8, (4 * newWidth),
                        CGColorSpace.CreateDeviceRGB(), CGImageAlphaInfo.PremultipliedFirst))
                    {
                        var imageRect = new RectangleF(0, 0, newWidth, newHeight);

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