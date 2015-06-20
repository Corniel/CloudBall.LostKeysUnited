using CloudBall.Engines.LostKeysUnited;

namespace CloudBall.Engines.LostKeysUnited
{
	public struct Wait : IAction
	{
		/// <summary>Invokes the wait action.</summary>
		/// <remarks>
		/// As don't calling this makes no difference, and calling sometimes
		/// ended up having null reference exceptions, the call itself is
		/// not executed.
		/// </remarks>
		public void Invoke(PlayerInfo player) {/* player.Player.ActionWait();*/ }

		public override string ToString() { return "Wait"; }
	}
}
