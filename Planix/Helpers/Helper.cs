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
    }
}
