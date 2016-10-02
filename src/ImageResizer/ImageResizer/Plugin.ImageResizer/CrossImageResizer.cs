using Plugin.ImageResizer.Abstractions;
using System;

namespace Plugin.ImageResizer
{
  /// <summary>
  /// Cross platform ImageResizer implemenations
  /// </summary>
  public class CrossImageResizer
  {
    static Lazy<IImageResizer> Implementation = new Lazy<IImageResizer>(() => CreateImageResizer(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// Current settings to use
    /// </summary>
    public static IImageResizer Current
    {
      get
      {
        var ret = Implementation.Value;
        if (ret == null)
        {
          throw NotImplementedInReferenceAssembly();
        }
        return ret;
      }
    }

    static IImageResizer CreateImageResizer()
    {
#if PORTABLE
        return null;
#else
        return new ImageResizerImplementation();
#endif
    }

    internal static Exception NotImplementedInReferenceAssembly()
    {
      return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
  }
}
