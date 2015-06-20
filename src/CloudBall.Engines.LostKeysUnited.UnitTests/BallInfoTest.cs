using Common;
using NUnit.Framework;

namespace CloudBall.Engines.LostKeysUnited.UnitTests
{
	[TestFixture]
	public class BallInfoTest
	{
		[Test]
		public void Accelaration_None_0f9930925()
		{
			var exp = Constants.BallSlowDownFactor;
			Assert.AreEqual(exp, BallInfo.Accelaration);
		}
		
		[Test]
		public void Accelaration_BallHalfTime_0f5()
		{
			var exp = 0.5f;
			var act = 1f;

			for (var i = 0; i < Constants.BallHalfTime; i++)
			{
				act *= BallInfo.Accelaration;
			}
			Assert.AreEqual(exp, act, 0.00001f);
		}
	}
}
