using System;
using System.Collections.Generic;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	/// <summary>The keeper is the last man standing, he will not leave his goal that easy.</summary>
	public class Keeper : IRole
	{
		public const Single MaximumDistanceFromGoal = 200f;
		public const Single MinimumDistanceFromBall = 60f;

		public PlayerInfo Apply(TurnInfos infos, IEnumerable<PlayerInfo> queue)
		{
			var keeper = Goal.Own.GetClosestBy(queue);

			var catcher = infos.CatchUp.Catcher;
			var ball = catcher.Key == null ? infos.Current.Ball.Position : catcher.Value.Position;

			var velocity = GetHalfwayVelocity(ball);

			var length = velocity.Speed.Value;

			var distance = Math.Max(MinimumDistanceFromBall, length - MaximumDistanceFromGoal);

			if (distance < 2 * MinimumDistanceFromBall)
			{
				keeper.Apply(Actions.Move(ball));
			}
			else
			{
				velocity = velocity.Scale(distance);
				var target = ball + velocity;
				keeper.Apply(Actions.Move(target));
			}
			return keeper;
		}

		public static Velocity GetHalfwayVelocity(Position ball)
		{
			var vectorTop = (ball - Goal.Own.Top);
			var vectorBot = (ball - Goal.Own.Bottom);

			// make of equal length.
			if (vectorTop.Speed > vectorBot.Speed)
			{
				vectorTop.Scale(vectorBot.Speed.Value);
			}
			else
			{
				vectorBot.Scale(vectorTop.Speed.Value);
			}

			// Get the point halfway.
			var halfway = (vectorTop - vectorBot) * 0.5f;
			var target = ball -(vectorTop - halfway);
			return target - ball;
		}
	}
}
