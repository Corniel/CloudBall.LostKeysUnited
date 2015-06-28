using CloudBall.Engines.LostKeysUnited.Models;
using CloudBall.Engines.LostKeysUnited.Roles;
using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	public class Default
	{
		public Default()
		{
			Roles = new List<IRole>()
			{
				Role.BallOwner,
				Role.PickUp,
				Role.BallCatcher,
				Role.Keeper,
			};
		}
		public List<IRole> Roles { get; set; }

		public bool Apply(GameState state, PlayerQueue queue)
		{
			foreach (var role in Roles)
			{
				role.Apply(state, queue);
			}
			var zones = Zones.Create(state);

			foreach (var zone in zones.SingleOccupiedByOwn)
			{
				var player = zone.Own.FirstOrDefault();
				if (queue.Contains(player))
				{
					queue.Dequeue(Actions.Move(player, zone.Target));
				}
			}
			foreach (var zone in zones.NotOccupiedByOwn)
			{
				var closedBy = zone.Target.GetClosedBy(queue);

				if (closedBy != null)
				{
					queue.Dequeue(Actions.Move(closedBy, zone.Target));
				}
			}
			if (zones.BallOwnerZone != null)
			{
				var closedBy = zones.BallOwnerZone.Target.GetClosedBy(queue);

				if (closedBy != null)
				{
					queue.Dequeue(Actions.Move(closedBy, zones.BallOwnerZone.Target));
				}
			}

			foreach (var player in queue.ToList())
			{
				queue.Dequeue(Actions.Wait(player));
			}
			return true;
		}
	}
}
