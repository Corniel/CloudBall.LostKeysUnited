using NUnit.Framework;

namespace CloudBall.Engines.LostKeysUnited.UnitTests
{
	[TestFixture]
	public class FieldInitSettingsTest
	{
		[Test]
		public void MaximumPassDistance_AvgPower6f0_500()
		{
			var settings = new FieldInitSettings()
				{
					ZoneSize = 20,
					MinimumPassDistance = Distance.Create(200),
					MaximumProgressLoss = 100f,
					PassPower = 7.5f,
					AveragePassPower = 6.0f,
					MaximumShootDistance = Distance.Create(700)
				};
			settings.Calculate();

			var actDis = settings.MaximumPassDistance;
			var expDis = Distance.Create(495);

			var actDur = settings.MaximumPassDuration;
			var expDur = 69;

			Assert.AreEqual(expDis.Value, actDis.Value, 1f, "Distance");
			Assert.AreEqual(expDur, actDur, "Duration");
		}
	}
}
