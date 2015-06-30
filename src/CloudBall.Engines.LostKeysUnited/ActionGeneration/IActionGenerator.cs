using CloudBall.Engines.LostKeysUnited.IActions;
using CloudBall.Engines.LostKeysUnited.Models;

namespace CloudBall.Engines.LostKeysUnited.ActionGeneration
{
	public interface IActionGenerator
	{
		void Generate(PlayerInfo player, GameState state, ActionCandidates candidates);
	}
}
