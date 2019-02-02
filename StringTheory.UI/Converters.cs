using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StringTheory.UI
{
    internal static class Converters
    {
        public static IValueConverter VisibleWhenTrue { get; } = new BooleanVisibilityConverter(trueValue: Visibility.Visible, falseValue: Visibility.Collapsed);
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    internal sealed class BooleanVisibilityConverter : IValueConverter
    {
        private static readonly object _boxedTrue = true;
        private static readonly object _boxedFalse = true;

        private readonly Visibility _trueValue;
        private readonly Visibility _falseValue;

        public BooleanVisibilityConverter(Visibility trueValue, Visibility falseValue)
        {
            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return b ? _trueValue : _falseValue;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v)
            {
                if (v == _trueValue) return _boxedTrue;
                if (v == _falseValue) return _boxedFalse;
            }

            return Binding.DoNothing;
        }
    }

    [ValueConversion(typeof(object), typeof(Visibility))]
    public sealed class CollapsedIfNullConverter : IValueConverter
    {
        public static readonly CollapsedIfNullConverter Default = new CollapsedIfNullConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    [ValueConversion(typeof(double), typeof(string))]
    public sealed class PercentageConverter : IValueConverter
    {
        public static readonly PercentageConverter Default = new PercentageConverter();
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d && d != 0)
            {
                return ((int) (d * 100)).ToString();
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}