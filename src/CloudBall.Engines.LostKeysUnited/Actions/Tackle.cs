using CloudBall.Engines.LostKeysUnited;

namespace CloudBall.Engines.LostKeysUnited
{
	public struct Tackle: IAction
	{
		private PlayerInfo other;

		public Tackle(PlayerInfo other) { this.other = other; }

		public void Invoke(PlayerInfo player) { player.Player.ActionTackle(other.Player); }
	}
}
