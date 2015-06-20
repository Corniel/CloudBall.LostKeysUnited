using CloudBall.Engines.LostKeysUnited;
using System;

namespace CloudBall.Engines.LostKeysUnited
{
	/// <summary>Shoots on a goal.</summary>
	public struct ShootOnGoal : IAction
	{
		/// <summary>The poser to shoot with.</summary>
		/// <remarks>
		/// TODO: This should be calculated.
		/// </remarks>
		private const Single power = 10f;

		/// <summary>Invokes the action.</summary>
		public void Invoke(PlayerInfo player) { player.Player.ActionShoot(Goal.Other.Center.ToVector(), power); }
	}
}
