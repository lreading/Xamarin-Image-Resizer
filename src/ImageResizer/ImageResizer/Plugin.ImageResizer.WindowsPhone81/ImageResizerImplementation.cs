using Plugin.ImageResizer.Abstractions;
using System;
using System.IO;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace Plugin.ImageResizer
{
    /// <summary>
    /// Implementation for ImageResizer
    /// </summary>
    public class ImageResizerImplementation : IImageResizer
    {
        public Task<byte[]> ResizeImageWithAspectRatioAsync(byte[] sourceImage, int maxWidth, int maxHeight)
        {
            throw new PlatformNotSupportedException();
        }
    }
}