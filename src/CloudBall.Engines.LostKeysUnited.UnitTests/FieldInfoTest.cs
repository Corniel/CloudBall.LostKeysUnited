using CloudBall.Engines.LostKeysUnited;
using NUnit.Framework;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.UnitTests
{
	[TestFixture]
	public class FieldInfoTest
	{
		[Test, Ignore]
		public void Create_20_IgnoreToBig()
		{
			var act = FieldInfo.Create(new FieldInitSettings()
			{
				ZoneSize = 20,
				MinimumPassDistance = Distance.Create(200),
				MaximumProgressLoss = 100f,
				PassPower = 7.5f,
				AveragePassPower = 6.0f,
				MaximumShootDistance = Distance.Create(700)
			});

			Assert.AreEqual(5184, act.Count, "act.Count");
			Assert.AreEqual(1822, act.Count(zone => zone.CanShotOnOtherGoal), "Count.CheckShotOnGoal");
			Assert.AreEqual(4.0, act.Sum(zone => zone.Neighbors.Length) / (double)act.Count, 0.1, "Avg.Neighbors");
			Assert.AreEqual(658.9, act.Sum(zone => zone.Targets.Count) / (double)act.Count, 10, "Avg.Neighbors");
		}

		[Test, Ignore]
		public void Create_40_WriteData()
		{
			var act = FieldInfo.Create(new FieldInitSettings()
				{
					ZoneSize = 40,
					MinimumPassDistance = Distance.Create(200),
					MaximumProgressLoss = 100f,
					PassPower = 7.5f,
					AveragePassPower = 6.0f,
					MaximumShootDistance = Distance.Create(700)
				});

			Assert.AreEqual(1296, act.Count, "act.Count");
			Assert.AreEqual(448, act.Count(zone => zone.CanShotOnOtherGoal), "Count.CheckShotOnGoal");
			Assert.AreEqual(3.88, act.Sum(zone => zone.Neighbors.Length) / (double)act.Count, 0.1, "Avg.Neighbors");
			Assert.AreEqual(171.2, act.Sum(zone => zone.Targets.Count) / (double)act.Count, 10, "Avg.Neighbors");

			act.Save(Bot.Location.FullName + ".1296.dat");
		}
		[Test]
		public void Create_120_WriteData()
		{
			var act = FieldInfo.Create(new FieldInitSettings()
			{
				ZoneSize = 120,
				MinimumPassDistance = Distance.Create(200),
				MaximumProgressLoss = 100f,
				PassPower = 7.5f,
				AveragePassPower = 6.0f,
				MaximumShootDistance = Distance.Create(700)
			});

			Assert.AreEqual(144, act.Count, "act.Count");
			Assert.AreEqual(52, act.Count(zone => zone.CanShotOnOtherGoal), "Count.CheckShotOnGoal");
			Assert.AreEqual(3.65, act.Sum(zone => zone.Neighbors.Length) / (double)act.Count, 0.1, "Avg.Neighbors");
			Assert.AreEqual(19.26, act.Sum(zone => zone.Targets.Count) / (double)act.Count, 10, "Avg.Neighbors");

			var path = Bot.Location.FullName + ".0144.dat";
			act.Save(path);
			var load = FieldInfo.Load(path);
		}


		[Test]
		public void GetQuadrant_somefields_DifferentQuadrants()
		{
			var field = new Position(13, 200);

			var left = new Position(-1, 180);
			var right = new Position(1930, 180);

			var above = new Position(20, -3);
			var under = new Position(50, 1090);

			var la = new Position(-1, -3);
			var lu = new Position(-1, 1082);

			var ra = new Position(2000, -3);
			var ru = new Position(3000, 1082);

			IsQuadrant(Quadrant.Field, field);
			IsQuadrant(Quadrant.Left, left);
			IsQuadrant(Quadrant.Right, right);
			IsQuadrant(Quadrant.Above, above);
			IsQuadrant(Quadrant.Under, under);

			IsQuadrant(Quadrant.Left | Quadrant.Above, la);
			IsQuadrant(Quadrant.Left | Quadrant.Under, lu);

			IsQuadrant(Quadrant.Right | Quadrant.Above, ra);
			IsQuadrant(Quadrant.Right | Quadrant.Under, ru);
		}
		private void IsQuadrant(Quadrant exp, Position field)
		{
			Assert.AreEqual(exp, Game.Field.GetQuadrant(field));
		}
	}
}
