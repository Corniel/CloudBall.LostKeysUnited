using CloudBall.Engines.LostKeysUnited.ActionGeneration;

namespace CloudBall.Engines.LostKeysUnited
{
	public static class Generator
	{
		public static readonly IActionGenerator ShootOnGoal = new ShootOnGoalGenerator();
		public static readonly IActionGenerator Dribble = new DribbleGenerator();
		public static readonly IActionGenerator Pass = new PassGenerator();
	}
}
