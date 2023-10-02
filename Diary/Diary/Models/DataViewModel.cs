using Diary.DataBase;

namespace Diary.Models
{
    public class DataViewModel
    {
        public List<Day> Days { get; set; }
        public int DayInMonth { get; set; }
        public int DayInWeek { get { return 7; } }
        public int WeeksInMounth { get { return 6; } }
        public DateTime FirstDayOfMonth { get; set; }
        public string Color
        {
            get
            {
                return _colors[_random.Next(0,_colors.Length)];
            }
        }

        private Random _random = new Random();
        private string[] _colors = new string[] { "Lavender", "LightSkyBlue", "LightSalmon", "DeepSkyBlue", "Plum", "Tomato", "SpringGreen", "Silver", "PaleGreen" };
    }
}
