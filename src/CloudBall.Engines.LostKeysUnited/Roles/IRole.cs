using CloudBall.Engines.LostKeysUnited.Models;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	public interface IRole
	{
		/// <summary>Applies the role on one of the players if possible.</summary>
		bool Apply(GameState state, PlayerQueue queue);
	}
}
