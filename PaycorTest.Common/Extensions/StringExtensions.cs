using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaycorTest.Common.Extensions
{
    public static class StringExtensions
    {
        public static string StringFromCollection(this IEnumerable<string> strings)
        {
            if(strings == null)
            {
                return null;
            }

            return string.Join(',', strings.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        public static List<string> ToStringList(this string str)
        {
            if(string.IsNullOrWhiteSpace(str))
            {
                return new List<string>();
            }

            var strings = str.Split(',', ' ').ToList();
            return strings.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        }
    }
}
