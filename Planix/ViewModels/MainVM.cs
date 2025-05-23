﻿using Planix.Helpers;
using Planix.Models;
using Planix.Resources.Ultilities;
using Planix.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

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

        private const double _EventCardWidth = 142.857;
        private const double _EventCardHeight = 60;
        private const double _EventDetailWindowWidth = 350;
        private const double _EventDetailWindowHeight = 500;
        private const double _PixelFromMainWindowTopToEventsGrid = 100;
        private const double _PixelFromMainWindowLeftToEventSGrid = 470;

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
            if (MainWindow.Instance == null)
            {
                return;
            }

            var eventDetail = new EventDetail(e);
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;

            var baseX = MainWindow.Instance.Left + _PixelFromMainWindowLeftToEventSGrid;
            var baseY = MainWindow.Instance.Top + _PixelFromMainWindowTopToEventsGrid;
            var eventPosition = Helper.GetEventPosition(e);

            var absoluteX = baseX + eventPosition.Left;
            var absoluteY = baseY + eventPosition.Top - MainWindow.Instance.EventsGrid.VerticalOffset;

            eventDetail.Left = absoluteX + _EventDetailWindowWidth > screenWidth 
                ? absoluteX - _EventDetailWindowWidth - _EventCardWidth 
                : absoluteX;

            eventDetail.Top = absoluteY + _EventDetailWindowHeight > screenHeight
                ? absoluteY - _EventDetailWindowHeight + _EventCardHeight - 3
                : absoluteY + 3;
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
