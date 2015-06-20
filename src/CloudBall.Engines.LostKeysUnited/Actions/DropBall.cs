using CloudBall.Engines.LostKeysUnited;

namespace CloudBall.Engines.LostKeysUnited
{
	public struct DropBall : IAction
	{
		public void Invoke(PlayerInfo player){player.Player.ActionDropBall();}

		public override string ToString() { return "DropBall"; }
	}
}
