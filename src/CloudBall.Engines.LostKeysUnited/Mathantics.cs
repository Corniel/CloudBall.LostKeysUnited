using System;

namespace CloudBall.Engines.LostKeysUnited
{
	public static class Mathematics
	{
		/// <summary>Returns the square root of a specified number.</summary>
		/// <remarks>
		/// Potentially introduce caching.
		/// </remarks>
		public static Single Sqrt(Single value)
		{
			return (Single)Math.Sqrt(value);
		}
	}
}
