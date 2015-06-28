using CloudBall.Engines.LostKeysUnited.Models;
using System;
using System.Collections.Generic;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	/// <summary>The keeper is the last man standing, he will not leave his goal that easy.</summary>
	public class Keeper : IRole
	{
		public const Single MaximumDistanceFromGoal = 210f;

		public bool Apply(GameState state, PlayerQueue queue)
		{
			var keeper = Goal.Own.GetClosestBy(queue);
			var ball = state.Current.Ball;

			if (keeper != null)
			{
				var velo = (ball.Position - keeper.Position).Scale(MaximumDistanceFromGoal);
				var target = Goal.Own.Center + velo;
				queue.Dequeue(Actions.Move(keeper, target));
			}
			return keeper != null;
		}
	}
}
