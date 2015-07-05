using CloudBall.Engines.LostKeysUnited.Roles;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBall.Engines.LostKeysUnited.UnitTests.Roles
{
	[TestFixture]
	public class KeeperTest
	{
		[Test]
		public void GetTarget_AlmostTopCornerFlag_LocialPosition()
		{
			var act = Keeper.GetTarget(new Position(10, 200));
			var exp = new Position(0.1f, 464.0f);

			CloudBallAssert.AreEqual(exp, act, 0.1f);
		}

		[Test]
		public void GetTarget_200x400y_LocialPosition()
		{
			var act = Keeper.GetTarget(new Position(200, 400));
			var exp = new Position(16.3f, 487.6f);

			CloudBallAssert.AreEqual(exp, act, 0.1f);
		}

		[Test]
		public void GetTarget_800x539y_LocialPosition()
		{
			var act = Keeper.GetTarget(new Position(800, 539));
			var exp = new Position(59.9f, 539.0f);

			CloudBallAssert.AreEqual(exp, act, 0.1f);
		}

		[Test]
		public void GetTarget_AlmostBottomCornerFlag_LocialPosition()
		{
			var act = Keeper.GetTarget(new Position(5, 900));
			var exp = new Position(0.1f, 614.0f);

			CloudBallAssert.AreEqual(exp, act, 0.1f);
		}
	}
}
