using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace PitWallAcquisitionPlugin.UI.Views
{
    public class ValidationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var casted = value as ReadOnlyObservableCollection<ValidationError>;

            if (casted == null || !casted.Any()) return null;

            ValidationError error = casted[0];

            return error.ErrorContent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
