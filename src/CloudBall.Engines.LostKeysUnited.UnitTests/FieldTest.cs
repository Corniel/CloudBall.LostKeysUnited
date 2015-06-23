using CloudBall.Engines.LostKeysUnited;
using NUnit.Framework;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.UnitTests
{
	[TestFixture]
	public class FieldTest
	{
		[Test]
		public void Create_20_()
		{
			var act = FieldInfo.Create(20);

			Assert.AreEqual(5184, act.Count, "act.Count");
			Assert.AreEqual(1822, act.Count(zone => zone.CheckShotOnGoal), "Count.CheckShotOnGoal");
			Assert.AreEqual(4.0, act.Sum(zone => zone.Neighbors.Length) / (double)act.Count, 0.1, "Avg.Neighbors");
			Assert.AreEqual(2010.0, act.Sum(zone => zone.Targets.Count) / (double)act.Count, 100, "Avg.Neighbors");
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
