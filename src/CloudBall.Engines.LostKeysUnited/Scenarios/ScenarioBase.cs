using CloudBall.Engines.LostKeysUnited.Roles;
using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	public abstract class ScenarioBase : IScenario
	{
		protected ScenarioBase()
		{
			Roles = new IRole[]
			{
				Role.PickUp,
				Role.BallCatcher,
				Role.Keeper,
			};
		}

		protected IRole[] Roles { get; set; }

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
			foreach (var role in Roles)
			{
				Dequeue(role.Apply(infos, Queue));
			}

			var closed = Goal.Other.GetClosestBy(Queue);
			if (closed != null)
			{
				var distance = Goal.Other.GetDistance(closed);
				if (distance > Distance.Create(400))
				{
					Dequeue(closed.Apply(Actions.Move(Goal.Other.Center - new Velocity(400, 0))));
				}
			}

		}
	}
}
