using System;
using System.Collections.Generic;

namespace CloudBall.Engines.LostKeysUnited
{
	/// <summary>Gate to System.Math, using singles instead of doubles.</summary>
	public static class Mathematics
	{
		public static readonly Single PI = (Single)Math.PI;

		public static Single Sqrt(double value)
		{
			return (Single)Math.Sqrt(value);
			//int key = (int)(10d * value + 4.99);
			//float sq;
			//if (!sqrt.TryGetValue(key, out sq))
			//{
			//	sq = (float)Math.Sqrt(key);
			//	sqrt[key] = sq;
			//}
			//return sq;
		}
		private static Dictionary<int, float> sqrt = new Dictionary<int, float>();
		public static int SqrtCount { get { return sqrt.Count; } }
	}
}
