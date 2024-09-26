using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleSharingSystem.MAUI.Converters
{
    public class SelectItemBorderColorConverter : IMultiValueConverter
    {
        private readonly Color customPrimaryColor;

        public SelectItemBorderColorConverter()
        {
            if (Application.Current?.Resources?.TryGetValue("CustomPrimaryColor", out var color) == true && color is Color validColor)
            {
                customPrimaryColor = validColor;
            }
            else
            {
                customPrimaryColor = Color.FromArgb("#8B5CF6");
            }
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] != null && values[1] != null)
            {
                var currentItem = values[0];
                var selectedItem = values[1];

                return currentItem.Equals(selectedItem) ? customPrimaryColor : Colors.Transparent;
            }
            return Colors.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
