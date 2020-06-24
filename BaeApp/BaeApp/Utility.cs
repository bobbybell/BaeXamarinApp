using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace IAScience
	{
	public class Utility
		{
		public static bool CloseValues(double d1, double d2)
			{
			return Math.Abs(d1 - d2) < 0.0001;
			}
		public static bool IsIntegerString(String s, out int theInt)
			{
			if (IsIntegerString(s)) {
				theInt = System.Convert.ToInt32(s);
				return true;
				}
			else {
				theInt = 0;
				return false;
				}
			}
		public static bool IsIntegerString(String s, out Int64 theInt)
			{
			if (IsIntegerString(s)) {
				theInt = System.Convert.ToInt64(s);
				return true;
				}
			else {
				theInt = 0;
				return false;
				}
			}
		public static bool IsSignChar(Char c)
			{
			return (c == '+') || (c == '-');
			}
		public static bool IsIntegerString(String s)
			{
			int length = s.Length;
			if (length == 0)
				return false;

			int index = 0;
			if (IsSignChar(s[0])) {
				if (length < 2)
					return false;
				index++;
				}

			while (index < length) {
				if (!Char.IsDigit(s[index]))
					return false;
				index++;
				}

			return true;
			}
		public static string SafeSubstring(string srcString, int startChar)
			{
			if (startChar < 0)
				startChar = 0;
			return srcString.Length <= startChar ? "" : srcString.Substring(startChar);
			}
		public static string SafeSubstring(string srcString, int startChar, int length)
			{
			if (length <= 0)
				return "";
			if (startChar < 0)
				startChar = 0;

			if (startChar >= srcString.Length)
				return "";

			if (srcString.Length - startChar <= length)
				return srcString.Substring(startChar);
			else
				return srcString.Substring(startChar, length);
			}
		public static string SafeRightString(string srcString, int length)
			{
			if (length <= 0)
				return "";
			return srcString.Length <= length ? srcString : srcString.Substring(srcString.Length - length, length);
			}
		}
	}
