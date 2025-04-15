using System.Windows;

namespace Planix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PowerOff_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Event_Detect(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}