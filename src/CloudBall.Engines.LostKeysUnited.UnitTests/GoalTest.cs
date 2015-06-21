using NUnit.Framework;

namespace CloudBall.Engines.LostKeysUnited.UnitTests
{
	[TestFixture]
	public class GoalTest
	{
		[Test]
		public void GetDistance_From3x150yToOwn_233f02()
		{
			var point = new Position(3, 150);

			var act = Goal.Own.GetDistance(point);
			var exp = Distance.Between(point, new Position(0, 383));

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void GetDistance_From130x450yToOwn_130f()
		{
			var point = new Position(130, 450);

			var act = Goal.Own.GetDistance(point);
			var exp = Distance.Create(130);

			Assert.AreEqual(exp, act);
		}
		[Test]
		public void GetDistance_From1500x750yToOwn_1501f()
		{
			var point = new Position(1500, 750);

			var act = Goal.Own.GetDistance(point);
			var exp = Distance.Between(point, new Position(0, 695));

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void GetClosestBy_3Points_130x450y()
		{
			var points = new Position[]
			{
				new Position(3, 150),
				new Position(130, 450),
				new Position(1500, 750),
			};

			var act = Goal.Own.GetClosestBy(points);
			var exp = points[1];

			Assert.AreEqual(exp, act);
		}
	}
}
