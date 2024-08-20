using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RS.ImageExplorer
{
    public class ThumbnailWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || !(values[0] is double) || !(values[1] is Thickness))
                return 170; // Default width

            double listBoxWidth = (double)values[0];
            Thickness padding = (Thickness)values[1];

            double availableWidth = listBoxWidth - padding.Left - padding.Right - 20; // 20 for scrollbar and some margin
            return Math.Max(availableWidth, 100); // Ensure minimum width of 100
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}