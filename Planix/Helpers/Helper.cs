using Planix.Models;

namespace Planix.Helpers
{
    public class Helper
    {
        public static double CellWidth = 142.857;

        public static Dictionary<DayOfWeek, double> DayOfWeekWidth = new Dictionary<DayOfWeek, double>
        {
            { DayOfWeek.Sunday,  0},
            { DayOfWeek.Monday, CellWidth },
            { DayOfWeek.Tuesday, 2 * CellWidth },
            { DayOfWeek.Wednesday, 3 * CellWidth },
            { DayOfWeek.Thursday, 4 * CellWidth },
            { DayOfWeek.Friday, 5 * CellWidth },
            { DayOfWeek.Saturday, 6 * CellWidth }
        };

        public static (double Left, double Top) GetEventPosition(Event e)
        {
            var left = DayOfWeekWidth[e.StartTime.DayOfWeek];
            var top = e.StartTime.Hour * 60 + e.StartTime.Minute;

            return (left, top);
        }

        //public static (double Width, double Height) GetCurrentScreenSize()
        //{
            
        //}
    }
}
