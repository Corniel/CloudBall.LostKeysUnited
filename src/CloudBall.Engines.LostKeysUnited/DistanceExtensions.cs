using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBall.Engines.LostKeysUnited
{
	public static class DistanceExtensions
	{
		public static Distance Average(this IEnumerable<Distance> distances)
		{
			return distances.Average(d => (double)d);
		}
		public static Distance Sum(this IEnumerable<Distance> distances)
		{
			return distances.Sum(d => (double)d);
		}
	}
}
