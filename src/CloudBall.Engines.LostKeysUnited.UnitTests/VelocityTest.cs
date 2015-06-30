using NUnit.Framework;
using System;

namespace CloudBall.Engines.LostKeysUnited.UnitTests
{
	[TestFixture]
	public class VelocityTest
	{
		public static readonly Velocity TestStruct = new Velocity(8.5f, 2.3f);

		[Test]
		public void Rotate_1Pi_Rotated180degrees()
		{
			var act = TestStruct.Rotate((Single)Math.PI);
			var exp = new Velocity(-8.5f, -2.3f);

			CloudBallAssert.AreEqual(exp, act);
		}

		[Test]
		public void Rotate_100x0yPlus45_71x71y()
		{
			var velocity = new Velocity(10, 0);
			var angle = new Angle(Math.PI / 4d);
			var act = velocity.Rotate(angle);
			var exp = new Velocity(Math.Sqrt(50), Math.Sqrt(50));

			CloudBallAssert.AreEqual(exp, act);
		}

		[Test]
		public void Rotate_100x0yPlus135_71x71y()
		{
			var velocity = new Velocity(10, 0);
			var angle = new Angle(3 * Math.PI / 4d);
			var act = velocity.Rotate(angle);
			var exp = new Velocity(-Math.Sqrt(50), Math.Sqrt(50));

			CloudBallAssert.AreEqual(exp, act);
		}

		[Test]
		public void Rotate_Minus45_71x71y()
		{
			var velocity = new Velocity(10, 0);
			var angle = new Angle(-Math.PI / 4d);
			var act = velocity.Rotate(angle);
			var exp = new Velocity(Math.Sqrt(50), -Math.Sqrt(50));

			CloudBallAssert.AreEqual(exp, act);
		}

		[Test]
		public void Rotate_Minus135_71x71y()
		{
			var velocity = new Velocity(10, 0);
			var angle = new Angle(-3 * Math.PI / 4d);
			var act = velocity.Rotate(angle);
			var exp = new Velocity(-Math.Sqrt(50), -Math.Sqrt(50));

			CloudBallAssert.AreEqual(exp, act);
		}

	}
}
