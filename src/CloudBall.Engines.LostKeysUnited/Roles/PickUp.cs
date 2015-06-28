using CloudBall.Engines.LostKeysUnited.Models;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	/// <summary>The player that can pick up the ball.</summary>
	public class PickUp : IRole
	{
		public bool Apply(GameState state, PlayerQueue queue)
		{
			var current = state.Current;
			if (current.Ball.IsOwn ||
				// if the other can not pick it up, and it's a goal, just let it go.
				(state.Path.End == BallPath.Ending.GoalOther &&
				!state.CatchUps.Any(cu => cu.Player.Team == TeamType.Other)))
			{
				return false;
			}

			var pickup = queue.FirstOrDefault(player => player.CanPickUpBall);
			if (pickup != null)
			{
				queue.Dequeue(Actions.PickUpBall(pickup));
			}
			return pickup != null;
		}
	}
}
