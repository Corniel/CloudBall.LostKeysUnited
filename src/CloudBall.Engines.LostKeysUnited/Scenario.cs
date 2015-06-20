using CloudBall.Engines.LostKeysUnited;
using CloudBall.Engines.LostKeysUnited.Scenarios;

namespace CloudBall.Engines.LostKeysUnited
{
	/// <summary>A container class with singleton instances of different implementations.</summary>
	public static class Scenario
	{
		public static readonly IScenario OnTheBall = new Scenarios.OnTheBall();
		public static readonly IScenario ShootOnGoal = new Scenarios.ShootOnGoal();
		public static readonly IScenario PickUpTheBall = new Scenarios.PickUpTheBall();
		public static readonly IScenario GetTheBall = new Scenarios.GetTheBall();
		public static readonly IScenario Wait = new Scenarios.Wait();
	}
}
