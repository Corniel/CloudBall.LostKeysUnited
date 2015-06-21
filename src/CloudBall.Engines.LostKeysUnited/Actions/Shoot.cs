using CloudBall.Engines.LostKeysUnited;
using System;

namespace CloudBall.Engines.LostKeysUnited
{
	/// <summary>Shoots on a goal.</summary>
	public struct Shoot : IAction
	{
		public Shoot(Position target, Single power) 
		{
			this.target = target;
			this.power = power; 
		}

		/// <summary>The poser to shoot with.</summary>
		private Position target;
		/// <summary>The power to shoot with.</summary>
		private Single power;

		/// <summary>Invokes the action.</summary>
		public void Invoke(PlayerInfo player) { player.Player.ActionShoot(target.ToVector(), power); }
	}
}
