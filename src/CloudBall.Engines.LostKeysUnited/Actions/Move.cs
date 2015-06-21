using CloudBall.Engines.LostKeysUnited;
using Common;

namespace CloudBall.Engines.LostKeysUnited
{
	public struct Move : IAction
	{
		private IPoint target;

		public Move(IPoint target) { this.target = target; }

		public void Invoke(PlayerInfo player) { player.Player.ActionGo(target.ToVector()); }
	}
}
