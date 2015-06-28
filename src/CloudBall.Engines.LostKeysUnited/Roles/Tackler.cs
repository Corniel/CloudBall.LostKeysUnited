using CloudBall.Engines.LostKeysUnited.Models;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	public class Tackler : IRole
	{
		public bool Apply(GameState state, PlayerQueue queue)
		{
			var current = state.Current;
			if (current.Ball.IsOther)
			{
				var tackle = queue.FirstOrDefault(player => player.CanTackle(current.Ball.Owner));
				if (tackle != null)
				{
					queue.Dequeue(Actions.Tacle(tackle, current.Ball.Owner));
					return true;
				}
			}
			if (current.Ball.Owner == null && state.CatchUps.Any(cu => cu.Player.Team == TeamType.Other))
			{
				var target = state.CatchUps.FirstOrDefault();
				if (target.Player.Team == TeamType.Other)
				{
					var tackle = queue.FirstOrDefault(player => player.CanTackle(target.Player));
					if (tackle != null)
					{
						queue.Dequeue(Actions.Tacle(tackle, target.Player));
						return true;
					}
				}
			}
			return false;
		}
	}
}
