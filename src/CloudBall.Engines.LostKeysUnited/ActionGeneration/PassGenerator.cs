using CloudBall.Engines.LostKeysUnited.IActions;
using CloudBall.Engines.LostKeysUnited.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBall.Engines.LostKeysUnited.ActionGeneration
{
	public class PassGenerator : IActionGenerator
	{
		public static readonly Power PassPower = 6.9f;
		public const int PickUpTimer = 7;
		public static readonly Distance MinimumPassDistance = 200d;

		public void Generate(PlayerInfo owner, GameState state, ActionCandidates candidates)
		{
			if (!owner.IsBallOwner) { return; }

			var maxTurns = (int)(double)owner.DistanceToOwnGoal / 5;

			foreach (var player in owner.GetOther(state.Current.OwnPlayers))
			{
				if (Distance.Between(owner, player) < MinimumPassDistance) { continue; }
				
				var velocity = Shoot.ToTarget(owner, player, PassPower);
				var pass = BallPath.Create(owner.Position, velocity, PickUpTimer, maxTurns);

				// We don't want to risk passes ending up in our own goal.
				if (pass.End != BallPath.Ending.GoalOwn)
				{
					var catchUp = pass.GetCatchUps(state.Current.Players).FirstOrDefault();

					// safe to pass.
					if (catchUp == null || catchUp.Player.IsOwn)
					{
						var length = catchUp == null ? pass.Count : catchUp.Turn;
						var target = catchUp == null ? pass.Last() : catchUp.Position;
						candidates.Add(Evaluator.GetPositionImprovement(player, target, length), Actions.Shoot(owner, target, PassPower));
					}
				}
			}
		}
	}
}
