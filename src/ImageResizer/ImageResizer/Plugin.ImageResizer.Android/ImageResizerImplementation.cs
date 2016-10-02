using Android.Graphics;
using Plugin.ImageResizer.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Plugin.ImageResizer
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class ImageResizerImplementation : IImageResizer
    {
        public async Task<byte[]> ResizeImageWithAspectRatioAsync(byte[] sourceImage, int maxWidth, int maxHeight)
        {
            return await Task.Run(() =>
            {
                using (var originalImage = BitmapFactory.DecodeByteArray(sourceImage, 0, sourceImage.Length))
                {
                    // Determine the ratio
                    var ratioX = (double)maxWidth / originalImage.Width;
                    var ratioY = (double)maxHeight / originalImage.Height;
                    var ratio = Math.Min(ratioX, ratioY);

                    var newWidth = (int)(originalImage.Width * ratio);
                    var newHeight = (int)(originalImage.Height * ratio);
                    using (var resizedImage = Bitmap.CreateScaledBitmap(originalImage, newWidth, newHeight, false))
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