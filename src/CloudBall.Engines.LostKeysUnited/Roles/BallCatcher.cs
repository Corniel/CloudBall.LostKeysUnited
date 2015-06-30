using CloudBall.Engines.LostKeysUnited.Models;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	public class BallCatcher : IRole
	{
		public bool Apply(GameState state, PlayerQueue queue)
		{
			var ball = state.Current.Ball;

			if (ball.HasOwner || state.Current.OwnPlayers.Any(player => player.CanPickUpBall)) { return false; }

			var catchUp = state.CatchUps.FirstOrDefault(c => queue.Contains(c.Player));

			if (catchUp != null)
			{
				queue.Dequeue(Actions.Move(catchUp.Player, catchUp.Position));
			}
			return catchUp != null;
		}
	}
}
