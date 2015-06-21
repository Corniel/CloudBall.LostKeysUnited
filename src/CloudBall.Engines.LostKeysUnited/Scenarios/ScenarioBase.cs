using CloudBall.Engines.LostKeysUnited.Roles;
using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	public abstract class ScenarioBase : IScenario
	{
		public ScenarioBase() { }

		protected List<PlayerInfo> Queue { get; set; }
		protected void Dequeue(PlayerInfo player)
		{
			if (player != null)
			{
				Queue.Remove(player);
			}
		}

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
			Dequeue(Role.PickUp.Apply(infos, Queue));
			Dequeue(Role.BallCatcher.Apply(infos, Queue));
			Dequeue(Role.Keeper.Apply(infos, Queue));
		}
	}
}
