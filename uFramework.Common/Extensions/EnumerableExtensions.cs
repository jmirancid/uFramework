﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uFramework.Common.Extensions
{
	public static class EnumerableExtensions
	{
		public static bool IsEmpty<T>(this IEnumerable<T> source)
		{
			return source == null || !source.Any();
		}
	}
}
