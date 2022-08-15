using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Util
{
	public enum CacheProfileDurations
	{
		Daily = 60 * 60 * 24,
		Weekly = 60 * 60 * 24 * 7,
		Monthly = 60 * 60 * 24 * 7 * 4,
		Yearly = 60 * 60 * 24 * 7 * 4 * 12

	}
}
