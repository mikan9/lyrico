using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lyrico.Extensions
{
    public static class StringExtensions
    {
        public static string ToUrlSafe(this string str, string separator)
        {
            return Regex.Replace(str, @"[^\w ]", "")
                .Replace("  ", separator)
                .Replace(" ", separator)
                .ToLower()
                .Trim();
        }
    }
}
