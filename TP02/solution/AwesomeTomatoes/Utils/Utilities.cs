using Colorful;
using System.Drawing;
using Console = Colorful.Console;

namespace AwesomeTomatoes
{
    public static class Utilities
    {
        public const string regexNumbersSquareBracket = "[\\[0-9\\]]";

        // TODO TP03: Aplicar as cores nas informações importantes
        public static StyleSheet ApplyStyleOnText(string regex, Color color)
        {
            StyleSheet styleSheet = new StyleSheet(Color.White);
            styleSheet.AddStyle(regex, color);
            return styleSheet;
        }

        public static void CenterText(String text)
        {
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop + 1);
            Console.WriteLine(text);
        }

        public static double DaysFromReleaseDate(DateTime releaseDate)
        {
            var today = DateTime.Now;
            var totalDaysFromRelease = (today - releaseDate).TotalDays;
            return totalDaysFromRelease;
        }

        public static double YearsFromReleaseDate(DateTime releaseDate)
        {
            var totalYearsFromRelease = DaysFromReleaseDate(releaseDate) / 365;
            return totalYearsFromRelease;
        }
    }
}
