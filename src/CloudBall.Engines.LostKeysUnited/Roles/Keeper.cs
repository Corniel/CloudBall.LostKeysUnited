using Common;
using System;
using System.Collections.Generic;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	/// <summary>The keeper is the last man standing, he will not leave his goal that easy.</summary>
	public class Keeper : IRole
	{
		public const Single MaximumDistanceFromGoal = 200f;
		public const Single MinimumDistanceFromBall = 60f;

		protected Keeper(PlayerInfo player)
		{
			Player = player;
		}
		public PlayerInfo Player { get; protected set; }

		public bool Apply(TurnInfos turns)
		{
			var catcher = turns.CatchUp.Catcher;
			var ball = catcher.Key == null ? turns.Current.Ball.Position : catcher.Value.Position;

			var velocity = GetHalfwayVelocity(ball);

			var length = velocity.Speed.GetValue();

			var distance = Math.Max(MinimumDistanceFromBall, length - MaximumDistanceFromGoal);

			if (distance < 2 * MinimumDistanceFromBall)
			{
				Player.Apply(Actions.Move(ball));
			}
			else
			{
				velocity = velocity.Scale(distance);
				var target = ball + velocity;
				Player.Apply(Actions.Move(target));
			}

			return true;
		}

		public static Velocity GetHalfwayVelocity(Position ball)
		{
			var vectorTop = (ball - Goal.Own.Top);
			var vectorBot = (ball - Goal.Own.Bottom);

			// make of equal length.
			if (vectorTop.Speed > vectorBot.Speed)
			{
				vectorTop.Scale(vectorBot.Speed.GetValue());
			}
			else
			{
				vectorBot.Scale(vectorTop.Speed.GetValue());
			}

			// Get the point halfway.
			var halfway = (vectorTop - vectorBot) * 0.5f;
			var target = ball -(vectorTop - halfway);
			return target - ball;
		}

		public static Keeper Select(IEnumerable<PlayerInfo> players)
		{
			var keeper = Goal.Own.GetClosestBy(players);
			return new Keeper(keeper);
		}
	}
}
