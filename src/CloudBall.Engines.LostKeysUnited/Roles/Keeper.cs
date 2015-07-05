using CloudBall.Engines.LostKeysUnited.Models;
using System;
using System.Collections.Generic;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	/// <summary>The keeper is the last man standing, he will not leave his goal that easy.</summary>
	public class Keeper : IRole
	{
		public const Single MaximumDistanceFromGoal = 75f;

		public bool Apply(GameState state, PlayerQueue queue)
		{
			if (queue.IsEmty) { return false; }

			var ball = state.Current.Ball.Position;

			if (!state.Current.Ball.HasOwner && state.CatchUps.Count > 0)
			{
				ball = state.CatchUps[0].Position;
			}

			var target = GetTarget(ball);

			var keeper = target.GetClosestBy(queue);

			queue.Dequeue(Actions.Move(keeper, target));
			
			return true;
		}

		public static Position GetTarget(Position ball)
		{
			var vTop = ball - Goal.Own.Top;
			var vBot = ball - Goal.Own.Bottom;
			var ang = Angle.Between(vTop, vBot);

			var velo = vBot.Rotate((float)ang / 2f);
			var move = velo.Scale(((float)vTop.Speed + (float)vBot.Speed) * 0.5 - MaximumDistanceFromGoal);
			var target = ball - move;
			return target;
		}
	}
}
