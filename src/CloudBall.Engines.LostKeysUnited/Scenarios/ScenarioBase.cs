using CloudBall.Engines.LostKeysUnited;
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
				Role.Sweeper,
				Role.Attacker,
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

			foreach (var player in Queue)
			{
				var source = Game.Field[player];

				var own = new HashSet<FieldZone>(){ Game.Field[player] };
				var other = new HashSet<FieldZone>();

				foreach(var p in player.GetOther(infos.Current.Players))
				{
					other.Add( Game.Field[p]);
				}
				var target = source
					.GetTargets(own, other)
					.OrderBy(z => Distance.Between(z, source))
					.FirstOrDefault();

				if (target != null)
				{
					Dequeue(player.Apply(Actions.Move(target)));
				}
			}
		}
	}
}
