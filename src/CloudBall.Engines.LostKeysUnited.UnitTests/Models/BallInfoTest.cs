using CloudBall.Engines.LostKeysUnited.Models;
using Common;
using NUnit.Framework;

namespace CloudBall.Engines.LostKeysUnited.UnitTests.Models
{
	[TestFixture]
	public class BallInfoTest
	{
		[Test]
		public void MaximumPickUpDistance_None_40()
		{
			var act = BallInfo.MaximumPickUpDistance;
			var exp = Constants.BallMaxPickUpDistance;
			Assert.AreEqual(exp, act);
		}
	}
}
