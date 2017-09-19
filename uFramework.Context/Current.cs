using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uFramework.Context
{
	public class Current
	{
		public static DateTime GetNowUTC
        {
            get
            {
                return DateTime.UtcNow;
            }
        }

	}
}