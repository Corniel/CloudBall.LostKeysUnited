using Common;
using NUnit.Framework;

namespace CloudBall.Engines.LostKeysUnited.UnitTests
{
	[TestFixture]
	public class PlayerInfoTest
	{
		[Test]
		public void MaximumVelocity_None_3f()
		{
			var exp = Constants.PlayerMaxVelocity;
			Assert.AreEqual(exp, PlayerInfo.MaximumVelocity);
		}

		[Test]
		public void AccelerationFactor_None_0f071773462()
		{
			var exp = Constants.PlayerAccelerationFactor;
			Assert.AreEqual(exp, PlayerInfo.AccelerationFactor);
		}

		[Test]
		public void SlowDownFactor_None_0f93303299f()
		{
			var exp = Constants.PlayerSlowDownFactor;
			Assert.AreEqual(exp, PlayerInfo.SlowDownFactor);
		}
	}
}
