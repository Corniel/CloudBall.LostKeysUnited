using CloudBall.Engines.LostKeysUnited;
using System;

namespace CloudBall.Engines.LostKeysUnited
{
	public static class Actions
	{
		public static IAction PickUpBall { get { return new PickUpBall(); } }
		public static IAction DropBall { get { return new DropBall(); } }
		public static IAction Wait { get { return new Wait(); } }

		public static IAction Move(IPoint position) { return new Move(position); }

		public static IAction ShootOnGoal() { return new ShootOnGoal(); }

		/// <summary>Shoots on open goal. No other player is closer to the goal.</summary>
		public static IAction ShootOnOpenGoal() { return new ShootOnOpenGoal(); }
		

		public static IAction Tacle(PlayerInfo other) { return new Tackle(other); }
	}
}
