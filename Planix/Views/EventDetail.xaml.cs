using Planix.Models;
using System.Windows;
using System.Windows.Input;

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

        private void PowerOff_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
