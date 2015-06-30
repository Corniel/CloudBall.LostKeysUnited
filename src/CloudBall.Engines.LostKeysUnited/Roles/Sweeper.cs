using CloudBall.Engines.LostKeysUnited.Models;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	public class Sweeper : IRole
	{
		public bool Apply(GameState state, PlayerQueue queue)
		{
			if (state.Current.Ball.IsOther)
			{
				var path = PlayerPath.Create(state.Current.Ball.Owner, Goal.Own.Center, 400, 40f);

				var sweeper = path.GetCatchUps(queue).FirstOrDefault();

				if (sweeper != null)
				{
					return queue.Dequeue(Actions.Move(sweeper.Player, sweeper.Position));
				}
			}
			return false;
		}
	}
}
