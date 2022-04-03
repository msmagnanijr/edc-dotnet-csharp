using Console = Colorful.Console;
using System.Text;

namespace Infrastructure.Utils;
public static class Utilities
{
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
    public static void SaveToTextFile(string content, string fileName)
    {
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        using (var stream = new FileStream(
            Path.Combine(docPath, fileName), FileMode.Append, FileAccess.Write, FileShare.Write, 4096))
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            stream.Write(bytes, 0, bytes.Length);
        }
    }
    public static List<string> ReadTextFromFile(String fileName)
    {
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        if (File.Exists(Path.Combine(docPath, fileName)))
        {
            List<string> lines = File.ReadAllLines(Path.Combine(docPath, fileName)).ToList();
            return lines;
        }
        else
        {
            return new List<string>();
        }
    }
    public static void RemoveLineFromFile(String fileName, string id) 
    {
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        List<string> lines = File.ReadAllLines(Path.Combine(docPath, fileName)).ToList();
        int index = 0;
        foreach (string line in lines)
        {
            if (line.Contains(id))
            {
                lines.RemoveAt(index);
                break;
            }
            index++;
        }
        File.WriteAllLines(Path.Combine(docPath, fileName), lines.ToArray());
    }
}
