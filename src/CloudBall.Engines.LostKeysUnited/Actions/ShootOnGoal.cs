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
		/// <summary>The power to shoot with.</summary>
		private static readonly Position target = Goal.Other.Center;

		/// <summary>Invokes the action.</summary>
		public void Invoke(PlayerInfo player) { player.Player.ActionShoot(target.ToVector(), power); }

		/// <summary>Represents the action as <see cref="System.String"/>.</summary>
		public override string ToString()
		{
			return String.Format("Shoot on goal {0}, power: {1}", target, power);
		}
	}
}
