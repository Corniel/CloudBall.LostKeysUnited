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
			Console.WriteLine("Sqrt.Count: {0}", Mathematics.SqrtCount);
		
		}
	}
}
