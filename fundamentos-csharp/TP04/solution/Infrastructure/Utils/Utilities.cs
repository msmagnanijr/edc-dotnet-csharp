using Colorful;
using System.Drawing;
using Console = Colorful.Console;
using System.IO;
using System.Text;

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

    public static double DaysFromReleaseDate(DateOnly releaseDate)
    {
        var releaseDateNow = releaseDate.ToDateTime(TimeOnly.Parse("00:00 AM"));
        var today = DateTime.Now;
        var totalDaysFromRelease = (today - releaseDateNow).TotalDays;
        return totalDaysFromRelease;
    }

    public static double YearsFromReleaseDate(DateOnly releaseDate)
    {
        var totalYearsFromRelease = DaysFromReleaseDate(releaseDate) / 365;
        return totalYearsFromRelease;
    }

    public static void SaveToTextFile(string content, string fileName)
    {
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        using (var stream = new FileStream(
            Path.Combine(docPath, fileName), FileMode.Append, FileAccess.Write, FileShare.Write, 4096))
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            stream.Write(bytes, 0, bytes.Length);
        }

    }

    public static List<string> ReadTextFromFile(String fileName)
    {
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        List<string> lines = File.ReadAllLines(Path.Combine(docPath, fileName)).ToList();
        return lines;

    }

    public static void RemoveLineFromFile(String fileName, string id)
    {
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        List<string> lines = File.ReadAllLines(Path.Combine(docPath, fileName)).ToList();
        lines.RemoveAt(1);
        File.WriteAllLines(Path.Combine(docPath, fileName), lines.ToArray());
    }
}
