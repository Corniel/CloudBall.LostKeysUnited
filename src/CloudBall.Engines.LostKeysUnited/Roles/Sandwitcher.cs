using CloudBall.Engines.LostKeysUnited.Models;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	/// <summary>The sandwicher helps the first man marker if the ball owner is
	/// running from the goal.
	/// </summary>
	public class Sandwicher : IRole
	{
		public bool Apply(GameState state, PlayerQueue queue)
		{
			if (!state.Current.Ball.IsOther || !queue.Any()) { return false; }

			var owner = state.Current.Ball.Owner;
			var target = owner.Position + owner.Velocity;

			// if the owner is moving away.
			if (owner.DistanceToOwnGoal > Goal.Own.GetDistance(target))
			{
				// The sandwich can only work if the sandwicher is coming from the other side.
				var sandwicher = owner.GetClosestBy(queue.Where(player => player.DistanceToOwnGoal > owner.DistanceToOwnGoal));
				if(sandwicher != null)
				{
					return queue.Dequeue(Actions.Move(sandwicher, owner));
				}
			}
			return false;
		}
	}
}
