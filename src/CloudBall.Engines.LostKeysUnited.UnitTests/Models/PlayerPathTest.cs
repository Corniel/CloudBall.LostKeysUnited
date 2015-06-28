using CloudBall.Engines.LostKeysUnited.Models;
using Common;
using NUnit.Framework;

namespace CloudBall.Engines.LostKeysUnited.UnitTests.Models
{
	[TestFixture]
	public class PlayerPathTest
	{
		[Test]
		public void MaximumVelocity_None_3f()
		{
			var exp = Constants.PlayerMaxVelocity;
			Assert.AreEqual(exp, PlayerPath.MaximumVelocity);
		}

		[Test]
		public void AccelerationFactor_None_0f071773462()
		{
			var exp = Constants.PlayerAccelerationFactor;
			Assert.AreEqual(exp, PlayerPath.AccelerationFactor);
		}

		[Test]
		public void SlowDownFactor_None_0f93303299f()
		{
			var exp = Constants.PlayerSlowDownFactor;
			Assert.AreEqual(exp, PlayerPath.SlowDownFactor);
		}

		[Test]
		public void Create_Player_ReachesTheTargetIn44Turns()
		{
			var player = new PlayerInfo()
			{
				Position = new Position(200, 200),
				Velocity = new Velocity(-2, 0),
			};

			var target = new Position(300, 200);

			var act = PlayerPath.Create(player, target, 100, Distance.Pickup);
			var exp = 44;
			Assert.AreEqual(exp, act.Count);
		}

		[Test]
		public void GetDistance_MaxSpeed1000Turns_3000()
		{
			var act = PlayerPath.GetDistance(3, 1000, 0);
			var exp = new Distance(3000);
			Assert.AreEqual(exp, act);
		}
	}
}
