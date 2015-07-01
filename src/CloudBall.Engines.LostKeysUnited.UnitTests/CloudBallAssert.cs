using NUnit.Framework;
using System;

namespace CloudBall.Engines.LostKeysUnited.UnitTests
{
	public static class CloudBallAssert
	{
		public static void AreEqual(Velocity expected, Velocity actual, float delta = 0.0001f)
		{
			var dX = Math.Abs(expected.X - actual.X);
			var dY = Math.Abs(expected.Y - actual.Y);
			if (dX > delta || dY > delta)
			{
				Assert.AreEqual(expected, actual);
			}
		}

		public static void AreEqual(IPoint expected, IPoint actual, float delta = 0.0001f)
		{
			var dX = Math.Abs(expected.X - actual.X);
			var dY = Math.Abs(expected.Y - actual.Y);
			if (dX > delta || dY > delta)
			{
				Assert.AreEqual(expected, actual);
			}
		}
	}
}
