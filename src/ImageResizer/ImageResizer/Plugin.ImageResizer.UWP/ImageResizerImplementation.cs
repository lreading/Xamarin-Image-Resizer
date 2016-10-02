using Plugin.ImageResizer.Abstractions;
using System;
using System.Threading.Tasks;

namespace Plugin.ImageResizer
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class ImageResizerImplementation : IImageResizer
    {
        public Task<byte[]> ResizeImageWithAspectRatioAsync(byte[] sourceImage, int maxWidth, int maxHeight)
        {
            throw new NotImplementedException();
        }
    }
}