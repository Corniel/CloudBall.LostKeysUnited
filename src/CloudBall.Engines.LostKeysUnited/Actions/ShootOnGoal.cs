using CloudBall.Engines.LostKeysUnited;
using System;

namespace CloudBall.Engines.LostKeysUnited
{
	/// <summary>Shoots on a goal.</summary>
	public struct ShootOnGoal : IAction
	{
		public ShootOnGoal(Single power) { this.power = power; }

		/// <summary>The power to shoot with.</summary>
		private Single power;

		/// <summary>Invokes the action.</summary>
		public void Invoke(PlayerInfo player) { player.Player.ActionShoot(Goal.Other.Center.ToVector(), power); }
	}
}
