using System;

namespace MiA_projekt.Extensions
{
    public static class StringExtensions
    {
        public static string ToPascalCase(this string s)
        {
            if (s.Length == 0)
                return String.Empty;

            return Char.ToUpperInvariant(s[0]) + s.Substring(1);
        }

        public static string ToCamelCase(this string s)
        {
            if (s.Length == 0)
                return String.Empty;

            return Char.ToLowerInvariant(s[0]) + s.Substring(1);
        }
    }
}
