using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace WebMvc.TagHelpers;

[HtmlTargetElement("timeago")]
public class TimeAgoTagHelper : TagHelper
{
    public DateTime Date { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "CustumTagHelper";
        output.TagMode = TagMode.StartTagAndEndTag;

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat($"<label> {RelativeDate(Date)}</label>");

        output.PreContent.SetHtmlContent(stringBuilder.ToString());
    }


    //https://stackoverflow.com/questions/11/calculate-relative-time-in-c-sharp
    public static string RelativeDate(DateTime value) 
    {
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;

        var ts = new TimeSpan(DateTime.UtcNow.Ticks - value.Ticks);
        double delta = Math.Abs(ts.TotalSeconds);

        if (delta < 1 * MINUTE)
            return ts.Seconds == 1 ? "um segundo atrás" : ts.Seconds + " segundos atrás";

        if (delta < 2 * MINUTE)
            return "um minuto atrás";

        if (delta < 45 * MINUTE)
            return ts.Minutes + " minutos atrás";

        if (delta < 90 * MINUTE)
            return "a uma hora atrás";

        if (delta < 24 * HOUR)
            return ts.Hours + " horas atrás";

        if (delta < 48 * HOUR)
            return "ontem";

        if (delta < 30 * DAY)
            return ts.Days + " dias atrás";

        if (delta < 12 * MONTH)
        {
            int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
            return months <= 1 ? "um mês atrás" : months + " meses atrás";
        }
        else
        {
            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "um ano atrás" : years + " anos atrás";
        }
    }
}

