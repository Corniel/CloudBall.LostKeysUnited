using CloudBall.Engines.LostKeysUnited;

namespace CloudBall.Engines.LostKeysUnited
{
	public struct PickUpBall : IAction
	{
		public void Invoke(PlayerInfo player) { player.Player.ActionPickUpBall(); }

		public override string ToString() { return "PickUpBall"; }
	}
}
