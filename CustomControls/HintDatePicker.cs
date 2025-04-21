using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CustomControls
{
    public class HintDatePicker : Control
    {
        static HintDatePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(HintDatePicker),
                new FrameworkPropertyMetadata(typeof(HintDatePicker))
            );
        }

        // Dependency Property cho SelectedDate
        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register(
                nameof(SelectedDate), 
                typeof(DateTime?), 
                typeof(HintDatePicker),
                new PropertyMetadata(null, OnSelectedDateChanged));

        public DateTime? SelectedDate
        {
            get => (DateTime?)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        // Callback khi SelectedDate thay đổi
        private static void OnSelectedDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as HintDatePicker;
            control?.UpdateDisplayText();
        }

        // Dependency Property cho CalendarStyle
        public static readonly DependencyProperty CalendarStyleProperty =
            DependencyProperty.Register(
                nameof(CalendarStyle), 
                typeof(Style), 
                typeof(HintDatePicker),
                new PropertyMetadata(null)
            );

        public Style CalendarStyle
        {
            get => (Style)GetValue(CalendarStyleProperty);
            set => SetValue(CalendarStyleProperty, value);
        }

        // Dependency Property cho CalendarButtonStyle
        public static readonly DependencyProperty CalendarButtonStyleProperty =
            DependencyProperty.Register(
                nameof(CalendarButtonStyle),
                typeof(Style),
                typeof(HintDatePicker),
                new PropertyMetadata(null)
            );

        public Style CalendarButtonStyle
        {
            get => (Style)GetValue(CalendarButtonStyleProperty);
            set => SetValue(CalendarButtonStyleProperty, value);
        }

        // Dependency Property cho CalendarDayButtonStyle
        public static readonly DependencyProperty CalendarDayButtonStyleProperty =
            DependencyProperty.Register(
                nameof(CalendarDayButtonStyle),
                typeof(Style),
                typeof(HintDatePicker),
                new PropertyMetadata(null)
            );

        public Style CalendarDayButtonStyle
        {
            get => (Style)GetValue(CalendarDayButtonStyleProperty);
            set => SetValue(CalendarDayButtonStyleProperty, value);
        }

        // Dependency Property cho IsPopupOpen
        public static readonly DependencyProperty IsPopupOpenProperty =
            DependencyProperty.Register(
                nameof(IsPopupOpen), 
                typeof(bool), 
                typeof(HintDatePicker),
                new PropertyMetadata(false)
            );

        public bool IsPopupOpen
        {
            get => (bool) GetValue(IsPopupOpenProperty);
            set => SetValue(IsPopupOpenProperty, value);
        }

        // Dependency Property cho DisplayText (hiển thị ngày)
        public static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register(
                nameof(DisplayText), 
                typeof(string), 
                typeof(HintDatePicker),
                new PropertyMetadata("dd/MM/yyyy")
            );

        public string DisplayText
        {
            get => (string)GetValue(DisplayTextProperty);
            set => SetValue(DisplayTextProperty, value);
        }

        public static readonly DependencyProperty OpenPopupCommandProperty =
            DependencyProperty.Register(
                nameof(OpenPopupCommand), 
                typeof(ICommand), 
                typeof(HintDatePicker),
                new PropertyMetadata(null)
            );

        public ICommand OpenPopupCommand
        {
            get => (ICommand)GetValue(OpenPopupCommandProperty);
            set => SetValue(OpenPopupCommandProperty, value);
        }

        public HintDatePicker()
        {
            // Command để mở Popup
            OpenPopupCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                IsPopupOpen = !IsPopupOpen;
            });
        }

        // Cập nhật DisplayText dựa trên SelectedDate
        private void UpdateDisplayText()
        {
            DisplayText = SelectedDate.HasValue ? SelectedDate.Value.ToString("dd/MM/yyyy") : "dd/MM/yyyy";
            Debug.WriteLine(DisplayText);
            IsPopupOpen = false;
        }
    }

    // RelayCommand đơn giản để xử lý command
    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T?> _execute;

        public RelayCommand(Predicate<T> canExecute, Action<T?> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _canExecute = canExecute;
            _execute = execute;
        }

        public bool CanExecute(object? parameter)
        {
            try
            {
                if (parameter == null)
                {
                    return true;
                }
                return _canExecute == null ? true : _canExecute((T)parameter);
            }
            catch
            {
                return true;
            }
        }

        public void Execute(object? parameter)
        {
            System.Diagnostics.Debug.WriteLine("Execute open popup command");
            _execute((T?)parameter);
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
