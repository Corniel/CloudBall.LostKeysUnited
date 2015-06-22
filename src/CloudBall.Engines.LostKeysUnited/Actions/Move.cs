using CloudBall.Engines.LostKeysUnited;
using Common;
using System;

namespace CloudBall.Engines.LostKeysUnited
{
	public struct Move : IAction
	{
		private IPoint target;

		public Move(IPoint target) { this.target = target; }

		public void Invoke(PlayerInfo player) { player.Player.ActionGo(target.ToVector()); }

		/// <summary>Represents the action as <see cref="System.String"/>.</summary>
		public override string ToString()
		{
			return String.Format("Move to  {0}", target);
		}
	}
}
