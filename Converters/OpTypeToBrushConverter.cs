using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace SteganographyLSB.Converters;

public class OpTypeToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string type)
        {
            return type switch
            {
                "Encode" => Brushes.DodgerBlue,
                "Decode" => Brushes.SpringGreen,
                _ => Brushes.Gray
            };
        }
        return Brushes.Gray;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
