using CloudBall.Engines.LostKeysUnited.IActions;
using CloudBall.Engines.LostKeysUnited.Models;
using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	public class BallOwner : IRole
	{
		public static readonly Angle TwoStandardDeviation = new Angle(0.4);

		public const float SafeTurnsFactor = 1;
		public const float LoseBallPenalty = 700;
		public const float GoalReward = 10000;

		public static readonly Distance MaximumPass = 700d;
		public static readonly Distance KeepRunningDistance = 100d;

		public bool Apply(GameState state, PlayerQueue queue)
		{
			var owner = queue.FirstOrDefault(player => player.IsBallOwner);

			if (owner == null) { return false; }
			var distanceToGoal = Goal.Other.GetDistance(owner);

			var options = new List<PotentialAction>();

			AddMoveActions(owner, distanceToGoal, state, options);
			AddShotOnGoal(owner, distanceToGoal, state, options);
			AddPasses(owner, distanceToGoal, state, options);

			options.Sort();
			var action = options[0].Action;
			queue.Dequeue(action);
			return options[0].IsNone;
		}

		public void AddMoveActions(PlayerInfo owner, Distance distanceToGoal, GameState state, List<PotentialAction> options)
		{
			
			for (var dX = 150; dX <= 300; dX += 150)
			{
				for (var dY = -300; dY <= 300; dY += 300)
				{
					var x = owner.Position.X + dX;
					var y = owner.Position.Y + dY;
					if (y > Game.Field.MinimumY && y < Game.Field.MaximumY &&
						x > Game.Field.MinimumX && x < Game.Field.MaximumX)
					{
						var target = new Position(x, y);
						var path = PlayerPath.Create(owner, target, 100, 0d);

						var catchUp = path.GetCatchUps(state.Current.OtherPlayers).FirstOrDefault();
						if(catchUp == null || catchUp.Turn > 5)
						{
							var turns = catchUp == null ? path.Count : catchUp.Turn - 1;
							var score = 0f;
							score += (float)Goal.Other.GetDistance(target) - (float)distanceToGoal;
							options.Add(new PotentialAction(score, Actions.Move(owner, target)));
						}
					}
				}
			}
		}

		public void AddShotOnGoal(PlayerInfo owner, Distance distanceToGoal, GameState state, List<PotentialAction> options)
		{
			if(distanceToGoal < 700d)
			{
				var veloTop = Goal.Other.Top - owner.Position;
				var veloBot = Goal.Other.Bottom - owner.Position;
				var goalAngle = Angle.Between(veloTop, veloBot);
				var power95Per = Power.Maximum * (0.5f * (float)goalAngle / (float)TwoStandardDeviation);

				var target = owner.Position + veloBot.Rotate((double)goalAngle * 0.5);

				var turns = (float)Distance.Between(owner, target) / (float)power95Per;

				if (turns < 8)
				{
					options.Add(new PotentialAction(GoalReward, Actions.Shoot(owner, target, power95Per)));
				}
				else
				{
					options.Add(new PotentialAction(0, Actions.ShootOnGoal(owner, Power.Maximum)));
				}
			}
			//options.Add(new PotentialAction(-GoalReward, Actions.Move(owner, Goal.Other.Center)));
			options.Add(new PotentialAction(-GoalReward, Actions.ShootOnGoal(owner, Power.Maximum)));
		}

		public void AddPasses(PlayerInfo owner, Distance distanceToGoal, GameState state, List<PotentialAction> options)
		{
			foreach (var player in owner.GetOther(state.Current.OwnPlayers))
			{
				var action = Actions.Shoot(owner, player, 7.5f);
				var score = GetPassScore(state, owner, action);
				if (!double.IsNaN(score))
				{
					options.Add(new PotentialAction((float)score, action));
				}
			}
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
				return ((double)Goal.Other.GetDistance(catchUp.Position) - (double)Goal.Other.GetDistance(owner)) + catchUp.Turn;
			}
			return double.NaN;
		}

	}
}
