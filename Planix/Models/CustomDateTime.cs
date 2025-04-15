using Planix.Resources.Ultilities;

namespace Planix.Models
{
    public class CustomDateTime : BaseViewModel
    {
        private DateTime _Date;
        public DateTime Date
        {
            get => _Date;
            set
            {
                if (SetProperty(ref _Date, value))
                {
                    OnPropertyChanged(nameof(DateStr));
                    OnPropertyChanged(nameof(IsDateTimeNow));
                }
            }
        }

        public string DateStr => $"{Date.DayOfWeek.ToString().Substring(0, 3).ToUpper()}-{Date.Day}";

        public bool IsDateTimeNow => DateTime.Now.ToShortDateString() == _Date.ToShortDateString();
    }
}
