using System;
using BinaryAnalysis.UnidecodeSharp;

namespace Delivery.BLL
{
    public class Transliterate
    {
        public static String PrettyUrl(string url)
        {
            return String.Format("{0}", url.Unidecode().ToLower().Replace(".", "").Replace(",", "").Replace(" ", "_").Replace("'", "").Replace("\"", ""));
        }

        public static String RusToEng(string value)
        {
            return value.Unidecode();
        }
    }
}