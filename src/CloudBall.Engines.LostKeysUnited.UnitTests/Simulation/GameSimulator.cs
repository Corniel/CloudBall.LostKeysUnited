using NUnit.Framework;
using System;

namespace CloudBall.Engines.LostKeysUnited.UnitTests.Simulation
{
	[TestFixture]
	public class GameSimulator
	{
		[Test]
		public void Run()
		{
			var red = new LostKeysUnited();
			var blue = new LostKeysUnited();
			using (var engine = new CloudBallEngine(red, blue))
			{
				engine.Run();
			}
		}
	}
}
