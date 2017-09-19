using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace uFramework.Common.Extensions
{
	public static class StringExtensions
	{
		public static string Strip(this string source, params char[] chars)
		{
			return new string(source.ToCharArray()
				.Where(c => !chars.Contains(c))
				.ToArray());
		}

		public static string ToSeoSlug(this string source)
		{
			source = source.ToLowerInvariant();

			var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(source);
			source = Encoding.ASCII.GetString(bytes);

			source = Regex.Replace(source, @"\s", "-", RegexOptions.Compiled);

			source = Regex.Replace(source, @"[^\w\s\p{Pd}]", "", RegexOptions.Compiled);

			source = source.Trim('-', '_');

			source = Regex.Replace(source, @"([-_]){2,}", "$1", RegexOptions.Compiled);

			return source;
		}

	}
}
