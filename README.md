# Xamarin Image Resizer
Cross platform Xamarin plugin to resize images on a mobile device.  This can be particularly useful when passing an image to an API.

#Platforms
 - Android
 - iOS (untested)
 - WindowsPhone8 (untested)
 
#TODO
 - UWP
 - WindowsPhone81
 - WindowsStore

#Installation
Install via NuGet: https://www.nuget.org/packages/Plugin.ImageResizer
    Install-Package Plugin.ImageResizer -Pre
As with any Xamarin Plugins, be sure to install the package to your shared code _and_ platform-specific implementations.

#How to Use
This can be used with a stream (if you're using the Media Plugin https://github.com/jamesmontemagno/MediaPlugin) or standard byte arrays.

    // If you already have the byte[]
    var resizedImage = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(originalImageBytes, 500, 1000);
    
    // If you have a stream, such as:
    // var file = await CrossMedia.Current.PickPhotoAsync(options);
    // var originalImageStream = file.GetStream();
    var resizedImage = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(originalImageStream, 500, 1000);



#Thanks
https://github.com/jamesmontemagno/Xamarin-Templates/tree/master/Plugins-Templates (Big shout out to James Montemagno for the work he's done with Xamarin, especially creating the concept of Xamarin Plugins)
https://github.com/xamarin/xamarin-forms-samples/tree/master/XamFormsImageResize
http://stackoverflow.com/a/6501997/3033053
