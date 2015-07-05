using CloudBall.Engines.LostKeysUnited.IActions;
using NUnit.Framework;
using System;

namespace CloudBall.Engines.LostKeysUnited.UnitTests.IActions
{
	[TestFixture]
	public class ShootTest
	{
		[Test]
		public void ToString_Action()
		{
			var action = new Shoot(2, new Position(1024, 512), 8f);
			var act = action.ToString();
			var exp = "Player[2] Shoots to (1024, 512) with power 8";

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void ToTarget_SomeWhere_XSqrt18YSqrt18()
		{
			var source = new Position(100, 100);
			var target = new Position(200, 200);
			var power = new Power(5);

			var act = Shoot.ToTarget(source, target, power);
			var exp = new Velocity(Math.Sqrt(18), Math.Sqrt(18));

			CloudBallAssert.AreEqual(exp, act);
		}

		[Test]
		public void WithVelocity_SomeWhere_X100plusSqrt17Y100plusSqrt18()
		{
			var source = new Position(100, 100);
			var target = new Velocity(15, 15);
			var power = new Power(5);

			var act = Shoot.WithVelocity(source, target, power);
			var exp = new Velocity(100d + Math.Sqrt(18), 100d+ Math.Sqrt(18));

			Assert.AreEqual(exp.X, act.X, 0.0001f);
			Assert.AreEqual(exp.Y, act.Y, 0.0001f);
		}
	}
}
