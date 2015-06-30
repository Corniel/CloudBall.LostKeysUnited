using CloudBall.Engines.LostKeysUnited.Models;

namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	public interface IScenario
	{
		bool Apply(GameState state, PlayerQueue queue);
	}
}
