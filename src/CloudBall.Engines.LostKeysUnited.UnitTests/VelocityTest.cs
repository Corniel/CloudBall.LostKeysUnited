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
			var exp = new Velocity(-8.5f, -2.299999f);

			Assert.AreEqual(exp.ToString(), act.ToString());
		}
	}
}
