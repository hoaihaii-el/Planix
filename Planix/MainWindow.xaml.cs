using Planix.ViewModels;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Planix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow? _instance;

        public static MainWindow? Instance
        {
            get => _instance;
            private set => _instance = value;
        }

        public MainWindow()
        {
            InitializeComponent();

            Instance = this;
            Instance.DataContext = new MainVM();
            _Timer = new DispatcherTimer();
            _Timer.Interval = TimeSpan.FromSeconds(60);
            _Timer.Tick += _Timer_Tick;
            _Timer.Start();
            Canvas.SetTop(TimeLine, DateTime.Now.Hour * 60 + DateTime.Now.Minute);
        }

        public double XLeft => base.Left;

        private void _Timer_Tick(object? sender, EventArgs e)
        {
            Canvas.SetTop(TimeLine, DateTime.Now.Hour * 60 + DateTime.Now.Minute);
        }

        private DispatcherTimer _Timer;

        private void PowerOff_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Event_Detect(object sender, MouseButtonEventArgs e)
        {
            var vm = this.DataContext as MainVM;
            if (vm == null)
            {
                return;
            }

            var clickedPosition = e.GetPosition((IInputElement)sender);

            double rowHeight = 60;
            double columnWidth = ((Label)sender).ActualWidth / 7;

            int dayIndex = (int)(clickedPosition.X / columnWidth);
            int hourIndex = (int)(clickedPosition.Y / rowHeight);
            double minute = (clickedPosition.Y % rowHeight) / rowHeight * 60;

            vm.AddEvent(dayIndex, hourIndex, minute < 30 ? 0 : 30);
        }

        private void PrevWeek_Click(object sender, MouseButtonEventArgs e)
        {
            var vm = this.DataContext as MainVM;
            if (vm == null)
            {
                return;
            }

            var dt = vm.PrevWeek();

            if (dt.Month != Calendar.DisplayDate.Month)
            {
                Calendar.DisplayDate = Calendar.DisplayDate.AddMonths(-1);
            }
        }

        private void NextWeek_Click(object sender, MouseButtonEventArgs e)
        {
            var vm = this.DataContext as MainVM;
            if (vm == null)
            {
                return;
            }

            var dt = vm.NextWeek();

            if (dt.Month != Calendar.DisplayDate.Month)
            {
                Calendar.DisplayDate = Calendar.DisplayDate.AddMonths(1);
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = this.DataContext as MainVM;
            if (vm == null || Calendar.SelectedDate == null)
            {
                return;
            }

            while (Calendar.SelectedDate.Value.Date < vm.CurrentWeek[0].Date.Date)
            {
                vm.PrevWeek();
            }

            while (Calendar.SelectedDate.Value.Date > vm.CurrentWeek[6].Date.Date)
            {
                vm.NextWeek();
            }
        }

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Instance = null;
        }
    }
}