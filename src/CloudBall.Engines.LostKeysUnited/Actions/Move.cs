using CloudBall.Engines.LostKeysUnited;
using Common;

namespace CloudBall.Engines.LostKeysUnited
{
	public struct Move : IAction
	{
		private IPoint position;

		public Move(IPoint position) { this.position = position; }

		public void Invoke(PlayerInfo player) { player.Player.ActionGo(position.ToVector()); }
	}
}
