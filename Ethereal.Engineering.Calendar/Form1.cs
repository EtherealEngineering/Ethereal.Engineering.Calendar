namespace Ethereal.Engineering.Calendar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string ToGalacticGaianCalendarFormat(DateTime date)
        {
            date = date.Date;

            var pisces = "♓";
            var aquarius = "♒";

            var galacticYear = 0;
            var galacticMonth = aquarius;

            var dayOfYear = date.DayOfYear;
            var year = date.Year - 2023;

            if (date.Month < 3 || (date.Month == 3 && date.Day < 21))
            {
                year--;
                dayOfYear = new DateTime(date.Year, 12, 31).DayOfYear - (new DateTime(date.Year, 3, 20).DayOfYear - date.DayOfYear);
            }
            else
            {
                dayOfYear = dayOfYear - new DateTime(date.Year, 3, 20).DayOfYear;
            }

            var month = MonthMapping(dayOfYear / 30);
            var dayOfMonth = dayOfYear % 30;

            if (year < 0)
            {
                galacticMonth = pisces;
                year = 2160 + year;
            }

            return $"{DecimalToDozenal(galacticYear)}{galacticMonth}{DecimalToDozenal(year)}{month}{DecimalToDozenal(dayOfMonth)}";
        }

        public static string MonthMapping(int month)
        {
            switch (month)
            {
                case 0:
                    return "♈";
                case 1:
                    return "♉";
                case 2:
                    return "♊";
                case 3:
                    return "♋";
                case 4:
                    return "♌";
                case 5:
                    return "♍";
                case 6:
                    return "♎";
                case 7:
                    return "♏";
                case 8:
                    return "♐";
                case 9:
                    return "♑";
                case 10:
                    return "♒";
                case 11:
                    return "♓";
                case 12:
                    return "⛎";
            }

            return "";
        }

        public static string DecimalToDozenal(int value)
        {
            return IntToStringFast(value, new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '↊', '↋' });
        }

        /// <summary>
        /// An optimized method using an array as buffer instead of 
        /// string concatenation. This is faster for return values having 
        /// a length > 1.
        /// </summary>
        public static string IntToStringFast(int value, char[] baseChars)
        {
            // 32 is the worst cast buffer size for base 2 and int.MaxValue
            int i = 32;
            char[] buffer = new char[i];
            int targetBase = baseChars.Length;

            do
            {
                buffer[--i] = baseChars[value % targetBase];
                value = value / targetBase;
            }
            while (value > 0);

            char[] result = new char[32 - i];
            Array.Copy(buffer, i, result, 0, 32 - i);

            return new string(result);
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox1.Text = ToGalacticGaianCalendarFormat(e.Start);
        }
    }
}