using CloudBall.Engines.LostKeysUnited;
using System;

namespace CloudBall.Engines.LostKeysUnited
{
	/// <summary>Shoots on open goal. No other player is closer to the goal.</summary>
	public struct ShootOnOpenGoal : IAction
	{
		/// <summary>The poser to shoot with.</summary>
		/// <remarks>
		/// TODO: This should be calculated.
		/// </remarks>
		private const Single power = 7f;

		/// <summary>Invokes the action.</summary>
		public void Invoke(PlayerInfo player) { player.Player.ActionShoot(Goal.Other.Center.ToVector(), power); }
	}
}
