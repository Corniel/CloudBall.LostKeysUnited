using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.UnitTests
{
	[TestFixture]
	public class FieldZoneTest
	{
		[Test]
		public void GetMaximumDistanceToPass_AvgSpeed8f0_682f56604()
		{
			var distance = 1.2f* 8f;
			var distances = new List<Single>() { distance };

			while (distances.Average() > 7.0)
			{
				distance *= BallInfo.Accelaration;
				distances.Add(distance);
			}

			var act = Distance.Create(distances.Sum());
			var exp = Distance.Create(685.195557f);

			Assert.AreEqual(exp, act, "Distance");
			Assert.AreEqual(98, distances.Count, "time");
		}

		[Test]
		public void GetMaximumDistanceFromGoal_AvgSpeed9f5_682f56604()
		{
			var distance = 12f;
			var distances = new List<Single>() { distance };

			while (distances.Average() > 9.5)
			{
				distance *= BallInfo.Accelaration;
				distances.Add(distance);
			}

			var act = Distance.Create(distances.Sum());
			var exp = Distance.Create(682.56604f);

			Assert.AreEqual(exp, act);
		}
	}
}
