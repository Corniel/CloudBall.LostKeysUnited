using CloudBall.Engines.LostKeysUnited;

namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	/// <summary>Fall back mechanism. Just let all players wait.</summary>
	public class Wait : IScenario
	{
		public bool Apply(TurnInfos infos)
		{
			var info = infos.Current;

			foreach (var player in info.OthePlayers)
			{
				player.Apply(Actions.Wait);
			}
			return true;
		}
	}
}
