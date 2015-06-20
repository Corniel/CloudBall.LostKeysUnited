using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited
{
	public interface IPoint
	{
		Single X { get; }
		Single Y { get; }
	}

	public static class PointExtensions
	{
		public static Vector ToVector(this IPoint point) { return new Vector(point.X, point.Y); }

		public static T GetClosestBy<T>(this IPoint target, IEnumerable<T> candidates) where T : IPoint
		{
			return candidates.OrderBy(candidate => Distance.Between(target, candidate)).FirstOrDefault();
		}
	}
}
