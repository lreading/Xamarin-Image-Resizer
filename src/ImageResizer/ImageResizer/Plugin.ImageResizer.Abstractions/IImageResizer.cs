using System.Threading.Tasks;

namespace Plugin.ImageResizer.Abstractions
{
  /// <summary>
  /// Interface for ImageResizer
  /// </summary>
  public interface IImageResizer
  {
        Task<byte[]> ResizeImageWithAspectRatioAsync(byte[] sourceImage, int maxWidth, int maxHeight);
  }
}
