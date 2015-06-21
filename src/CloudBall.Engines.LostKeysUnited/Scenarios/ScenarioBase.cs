using CloudBall.Engines.LostKeysUnited.Roles;
using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	public abstract class ScenarioBase : IScenario
	{
		public ScenarioBase() { }

		protected List<PlayerInfo> Queue { get; set; }
		protected void Dequeue(PlayerInfo player){Queue.Remove(player);}

		public bool Apply(TurnInfos infos)
		{
			Queue = infos.Current.OwnPlayers.ToList();

			if (!ApplyScenario(infos)) { return false; }

			ApplyDefault(infos);
			return true;
		}
		protected abstract bool ApplyScenario(TurnInfos infos);

		protected virtual void ApplyDefault(TurnInfos infos)
		{
			var keeper = Keeper.Select(Queue);
			keeper.Apply(infos);
			Dequeue(keeper.Player);

			// TODO: don't spoil.
			foreach (var player in Queue)
			{
				player.Apply(Actions.Wait);
			}
		}
	}
}
