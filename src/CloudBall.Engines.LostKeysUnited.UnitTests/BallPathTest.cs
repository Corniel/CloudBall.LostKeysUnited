using NUnit.Framework;

namespace CloudBall.Engines.LostKeysUnited.UnitTests
{
	[TestFixture]
	public class BallPathTest
	{
		[Test]
		public void GetCatchUp_BallOnSomeDistance_After50Turns()
		{
			var turns = new TurnInfos();
			turns.Add(new TurnInfo()
			{
				Turn = 0,
				Ball = Stub.NewBall(300, 400, 10, 3),
			});

			var player = Stub.NewPlayer(800f, 650f, 0.3f, 0f, 3, TeamType.Own);

			var act = turns.BallPath.GetCatchUp(player);
			var exp = new TurnPosition(50, new Position(724.0216f, 527.2064f));

			Assert.AreEqual(exp, act);
		}
	}
}
