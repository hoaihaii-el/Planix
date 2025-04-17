using Planix.Models;
using Planix.Resources.Ultilities;
using Planix.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Planix.ViewModels
{
    public class MainVM : BaseViewModel
    {
        public ObservableCollection<string> Hours { get; set; } = new ObservableCollection<string>()
        {
            "1AM","2AM","3AM","4AM","5AM","6AM","7AM","8AM","9AM","10AM","11AM","12PM",
            "1PM","2PM","3PM","4PM","5PM","6PM","7PM","8PM","9PM","10PM","11PM",
        };
        public ObservableCollection<int> Days { get; set; } = new ObservableCollection<int>()
        {
            1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,
            1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,
            1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,
            1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,
            1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,
            1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,
            1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,
        };


        private ObservableCollection<CustomDateTime> _CurrentWeek = new ObservableCollection<CustomDateTime>();
        public ObservableCollection<CustomDateTime> CurrentWeek
        {
            get => _CurrentWeek;
            set => SetProperty(ref _CurrentWeek, value);
        }

        private ObservableCollection<Event> _Events = new ObservableCollection<Event>();
        public ObservableCollection<Event> Events
        {
            get => _Events;
            set => SetProperty(ref _Events, value);
        }

        public ICommand EventItemClickCM { get; set; }

        public MainVM()
        {
            InitializeWeek();

            var date = DateTime.Now;
            Events.Add(new Event
            {
                ID = "1",
                Title = "Test Event",
                StartTime = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0),
                EndTime = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0),
                Location = "Online",
                MeetingURL = "https://example.com"
            });

            EventItemClickCM = new RelayCommand<Event>((p) => true, (p) =>
            {
                OpenEventDetail(p);
            });
        }

        private void OpenEventDetail(Event e)
        {
            var eventDetail = new EventDetail(e);
            eventDetail.Left = e.Left + 645;

            var screenHeight = SystemParameters.PrimaryScreenHeight;
            if (MainWindow.LastMouseClickPostition.Y + eventDetail.Height > screenHeight)
            {
                eventDetail.Top = MainWindow.LastMouseClickPostition.Y - 450;
            }
            else
            {
                eventDetail.Top = MainWindow.LastMouseClickPostition.Y;
            }

            eventDetail.ShowDialog();
        }

        public void InitializeWeek()
        {
            var firstDay = DateTime.Now;
            while (firstDay.DayOfWeek != DayOfWeek.Sunday)
            {
                firstDay = firstDay.AddDays(-1);
            }

            for (int i = 0; i < 7; i++)
            {
                CurrentWeek.Add(new CustomDateTime
                {
                    Date = firstDay.AddDays(i)
                });
            }
        }

        public DateTime PrevWeek()
        {
            var dt = CurrentWeek[0].Date;
            CurrentWeek.Clear();

            for (int i = 1; i <= 7; i++)
            {
                CurrentWeek.Insert(0, new CustomDateTime
                {
                    Date = dt.AddDays(i * -1)
                });
            }

            // GetEventsData

            return CurrentWeek[CurrentWeek.Count - 1].Date;
        }

        public DateTime NextWeek()
        {
            var dt = CurrentWeek[CurrentWeek.Count - 1].Date;
            CurrentWeek.Clear();

            for (int i = 1; i <= 7; i++)
            {
                CurrentWeek.Add(new CustomDateTime
                {
                    Date = dt.AddDays(i)
                });
            }

            // GetEventsData

            return CurrentWeek[CurrentWeek.Count - 1].Date;
        }

        public void AddEvent(int dayIndex, int hour, int min)
        {
            var date = CurrentWeek[dayIndex].Date;
            var newEvent = new Event
            {
                Title = "Empty Event",
                StartTime = new DateTime(date.Year, date.Month, date.Day, hour, min, 0)
            };
            newEvent.EndTime = newEvent.StartTime.AddHours(1);

            Events.Add(newEvent);
        }
    }
}
