using CloudBall.Engines.LostKeysUnited;

namespace CloudBall.Engines.LostKeysUnited
{
	/// <summary>Represents a (player) action.</summary>
	/// <remarks>
	/// Those actions map to methods that can be applied to <see cref="Common.Player"/>.
	/// </remarks>
	public interface IAction
	{
		/// <summary>Invokes the action.</summary>
		void Invoke(PlayerInfo player);
	}
}
