using System;

namespace CloudBall.Engines.LostKeysUnited
{
	/// <summary>Mapper to Math, using singles instead of doubles.</summary>
	public static class Mathematics
	{
		public static Single Sqrt(Single value)
		{
			return (Single)Math.Sqrt(value);
		}
		public static Single Cos(Single value)
		{
			return (Single)Math.Cos(value);
		}
		public static Single Sin(Single value)
		{
			return (Single)Math.Sin(value);
		}
		public static Single Atan2(Single x, Single y)
		{
			return (Single)Math.Atan2(x, y);
		}

		
	}
}
