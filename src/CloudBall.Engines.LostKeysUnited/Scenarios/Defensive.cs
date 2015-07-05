using CloudBall.Engines.LostKeysUnited.IActions;
using CloudBall.Engines.LostKeysUnited.Models;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	public class Defensive : IScenario
	{
		public bool Apply(GameState state, PlayerQueue queue)
		{
			if (state.Opposition != TeamType.Other) { return false; }

			var other = state.Current.OtherPlayers.OrderBy(player => player.DistanceToOwnGoal).ToList();

			if(Role.Tackler.Apply(state, queue))
			{
				other.Remove(other.FirstOrDefault(item=> item.Id == ((Tackle)queue.Actions[0]).Target));
			}
			if(Role.Sweeper.Apply(state, queue))
			{
				other.Remove(state.Current.Ball.Owner);
			}
			
			Role.BallCatcher.Apply(state, queue);
			Role.Keeper.Apply(state, queue);

			foreach (var target in other)
			{
				if (queue.Count < 3) { break; }

				var defender = target.GetClosestBy(queue);
				queue.Dequeue(Actions.Move(defender, target));
			}
			return true;
		}
	}
}
