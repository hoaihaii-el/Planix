using Planix.Models;
using System.Windows;

namespace Planix.Views
{
    /// <summary>
    /// Interaction logic for EventDetail.xaml
    /// </summary>
    public partial class EventDetail : Window
    {
        public EventDetail()
        {
            InitializeComponent();
        }

        public EventDetail(Event e)
        {
            InitializeComponent();
            this.DataContext = e;
        }

        private void PowerOff_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
