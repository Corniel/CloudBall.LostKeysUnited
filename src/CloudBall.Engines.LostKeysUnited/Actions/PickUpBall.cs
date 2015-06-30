using CloudBall.Engines.LostKeysUnited;

namespace CloudBall.Engines.LostKeysUnited
{
	public struct PickUpBall : IAction
	{
		public void Invoke(PlayerInfo player) { player.Player.ActionPickUpBall(); }

		/// <summary>Represents the action as <see cref="System.String"/>.</summary>
		public override string ToString() { return "PickUpBall"; }
	}
}
