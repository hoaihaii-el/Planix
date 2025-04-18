using System.Windows;
using System.Windows.Controls;

namespace CustomControls
{
    public class PlaceholderTextBox : TextBox
    {
        static PlaceholderTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(PlaceholderTextBox), 
                new FrameworkPropertyMetadata(typeof(PlaceholderTextBox))
            );
        }

        // Dependency Property cho PlaceholderText
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                nameof(PlaceholderText), 
                typeof(string), 
                typeof(PlaceholderTextBox),
                new PropertyMetadata("Enter text here...")
            );

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }
    }
}
