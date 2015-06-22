using CloudBall.Engines.LostKeysUnited;
using System;

namespace CloudBall.Engines.LostKeysUnited
{
	public struct Wait : IAction
	{
		/// <summary>Invokes the wait action.</summary>
		/// <remarks>
		/// Calling sometimes ended up having null reference exceptions.
		/// </remarks>
		public void Invoke(PlayerInfo player)
		{
			try { player.Player.ActionWait(); }
			catch (NullReferenceException) { }
		}

		/// <summary>Represents the action as <see cref="System.String"/>.</summary>
		public override string ToString() { return "Wait"; }
	}
}
