using CloudBall.Engines.LostKeysUnited.IActions;
using CloudBall.Engines.LostKeysUnited.Models;
using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	public class BallOwner : IRole
	{
		public static readonly Distance MaximumPass = 700d;
		public static readonly Distance KeepRunningDistance = 100d;

		public bool Apply(GameState state, PlayerQueue queue)
		{
			var owner = queue.FirstOrDefault(player => player.IsBallOwner);

			if (owner != null)
			{
				if (Goal.Other.GetDistance(owner) < new Distance(300))
				{
					queue.Dequeue(Actions.ShootOnGoal(owner, Power.Maximum));
					return true;
				}

				var outcomes = new Dictionary<IAction, double>();
				
				var distanceToGoal = Goal.Other.GetDistance(owner);
				var closerToGoalOppos = state.Current.OtherPlayers.Where(p => Goal.Other.GetDistance(p) < distanceToGoal).ToList();

				if (closerToGoalOppos.All(p => Distance.Between(owner, p) > KeepRunningDistance))
				{
					outcomes[Actions.Move(owner, Goal.Other.Center)] = 5;
				}

				foreach (var player in owner.GetOther(state.Current.OwnPlayers))
				{
					var action = Actions.Shoot(owner, player, 6.9f);
					var score = GetPassScore(state, owner, action);
					if (!double.IsNaN(score))
					{
						outcomes[action] = score;
					}
				}

				if (outcomes.Any())
				{
					var best = outcomes.OrderByDescending(kvp => kvp.Value).FirstOrDefault();
					queue.Dequeue(best.Key);
					return true;
				}
				queue.Dequeue(Actions.ShootOnGoal(owner, Power.Maximum));
			}
			return owner != null;
		}

		public double GetPassScore(GameState state, PlayerInfo owner, Shoot action)
		{
			var distance = Distance.Between(owner, action.Target);
			if (distance > MaximumPass) { return double.NaN; }

			var turns = (double)distance / (double)action.Power;

			var velocity = action.Power.ToVelocity(owner, action.Target);

			var path = BallPath.Create(owner.Position, velocity, 7, (int)turns);
			var catchUps = path.GetCatchUps(owner.GetOther(state.Current.Players)).ToList();
			var catchUp = catchUps.FirstOrDefault();
			if (catchUp != null && catchUp.Player.Team == TeamType.Own)
			{
				return ((double)Goal.Other.GetDistance(catchUp.Position) - (double)Goal.Other.GetDistance(owner)) / catchUp.Turn;
			}
			return double.NaN;
		}
	}
}
