using System.Text.RegularExpressions;
using UnidecodeSharpFork;

namespace DeliveryNET.Core
{
    public static class StringExtensions
    {
        public static string HtmlToPureText(this string value)
        {
            value = Regex.Replace(value, @"<p>|</p>|<br>|<br />", "\r\n");
            value = Regex.Replace(value, @"<.+?>", string.Empty);
            value = value
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty)
                .Replace("&laquo;", string.Empty)
                .Replace("&raquo;", string.Empty)
                .Replace("&nbsp;", string.Empty)
                .Replace("&#39;", string.Empty)
                .Replace("&mdash;", string.Empty);
            return value;
        }

        public static bool IsEmail(this string value)
        {
            const string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            var match = Regex.Match(value.Trim(), pattern, RegexOptions.IgnoreCase);
            return match.Success;
        }

        public static string ToTransliterate(this string value)
        {
            return string.Format("{0}",
                value
                .Trim()
                .Unidecode()
                    .ToLower()
                    .Replace(".", "")
                    .Replace(",", "")
                    .Replace(":", "")
                    .Replace(";", "")
                    .Replace(" ", "_")
                    .Replace("'", "")
                    .Replace("\"", "")
                    .Replace("!", "")
                    .Replace("?", "")
                    .Replace("»", "")
                    .Replace("«", "")
                    .Replace(">", "")
                    .Replace("<", ""));
        }
    }
}
