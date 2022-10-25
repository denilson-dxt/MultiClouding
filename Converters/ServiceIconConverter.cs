using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Shared.PlatformSupport;

namespace MultiClouding.Converters;

public class ServiceIconConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var iconPath = value as string;
        var locator = AvaloniaLocator.Current.GetService<IAssetLoader>();
        Bitmap icon;
        try
        {
            icon = new Bitmap(locator.Open(new Uri(@"avares://MultiClouding/Assets/" + iconPath)));
        }
        catch (Exception e)
        {
           icon = new Bitmap(locator.Open(new Uri(@"avares://MultiClouding/Assets/avalonia-logo.ico")));
        }
        return icon;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}