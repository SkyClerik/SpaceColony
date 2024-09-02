using System.Linq;
using System.Text.RegularExpressions;

namespace SkyClericExt
{
    public static class StringExt
    {
        /// <summary>
        /// Возвращает значение, указывающее, содержит ли данная строка какую-ни будь из переданных подстрок.
        /// </summary>
        public static bool ContainsAny(this string str, params string[] values)
        {
            return values.Any(s => str.Contains(s));
        }

        /// <summary>
        /// Делит число запятыми, стеками по три
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToPriceStyle(int value)
        {
            string result = value.ToString();
            for (int count = result.Length, i = 3, t = 3; count >= i; i += 3, t += 4)
                result = result.Insert(result.Length - t, " ");

            return result;
        }

        public static string ToPriceStyle(this string str)
        {
            for (int count = str.Length, i = 3, t = 3; count >= i; i += 3, t += 4)
                str = str.Insert(str.Length - t, " ");

            return str;
        }

        public static bool IsValidEmail(this string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}