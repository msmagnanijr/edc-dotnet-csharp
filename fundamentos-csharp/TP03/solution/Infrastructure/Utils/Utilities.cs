using Colorful;
using System.Drawing;
using Console = Colorful.Console;

namespace Infrastructure.Utils;

public static class Utilities
{
    public const string regexNumbersSquareBracket = "[\\[0-9\\]]";
    public static StyleSheet ApplyStyleOnText(string regex, Color color)
    {
        StyleSheet styleSheet = new(Color.White);
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
