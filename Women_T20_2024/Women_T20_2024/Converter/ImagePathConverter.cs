using System.Windows.Data;

namespace Women_T20_2024
{
    public class ImagePathConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string imageName)
            {
                // Adjust the path as necessary
                string imagePath = $"Images/{imageName}";
                return imagePath;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}