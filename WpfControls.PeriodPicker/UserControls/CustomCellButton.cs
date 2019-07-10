using System.Windows;
using System.Windows.Controls;

namespace WpfControls.PeriodPicker.UserControls
{
    public class CustomCellButton:Button
    {
        public static readonly DependencyProperty PeriodIdProperty = DependencyProperty.Register("PeriodId", typeof(int?), typeof(CustomCellButton), new PropertyMetadata(null));
        public static readonly DependencyProperty InputValueProperty = DependencyProperty.Register("InputValue", typeof(string), typeof(CustomCellButton), new PropertyMetadata(string.Empty));

        public int? PeriodId
        {
            get => (int?)GetValue(PeriodIdProperty);
            set => SetValue(PeriodIdProperty, value);
        }

        public string InputValue
        {
            get => (string)GetValue(InputValueProperty);
            set => SetValue(InputValueProperty, value);
        }
    }
}