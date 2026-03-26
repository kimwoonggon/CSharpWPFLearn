using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfDataBindingLearn;

public class PercentageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double doubleValue)
        {
            return $"{doubleValue:0}%";
        }

        if (value is int intValue)
        {
            return $"{intValue}%";
        }

        return "0%";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string text && text.EndsWith("%") &&
            double.TryParse(text.TrimEnd('%'), out var result))
        {
            return result;
        }

        return 0d;
    }
}