using CloudBall.Engines.LostKeysUnited.IActions;
using CloudBall.Engines.LostKeysUnited.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBall.Engines.LostKeysUnited.ActionGeneration
{
	public class ShootOnGoalGenerator : IActionGenerator
	{
		public static readonly Distance MaximumShootDistance = 600d;
		public static readonly Position TopCorner = Goal.Other.Top + new Velocity(0, 60);
		public static readonly Position BottomCorner = Goal.Other.Bottom - new Velocity(0, 60);
		public static readonly Position[] GoalTargets = new Position[] { Goal.Other.Center, TopCorner, BottomCorner };
		public static readonly Angle TwoStandardDeviation = new Angle(0.4);
		public const int PickUpTimer = 7;

		public static readonly Power[] ShootPowers = new Power[]{ 7.0f, 7.6f, 8.3f, 9.1f, 10f };

		public void Generate(PlayerInfo owner, GameState state, ActionCandidates candidates)
		{
			if (!owner.IsBallOwner || owner.DistanceToOtherGoal > MaximumShootDistance) { return; }

			var veloTop = Goal.Other.Top - owner.Position;
			var veloBot = Goal.Other.Bottom - owner.Position;
			var goalAngle = Angle.Between(veloTop, veloBot);
			var power95Per = Power.Maximum * (0.5f * (float)goalAngle / (float)TwoStandardDeviation);

			var target = owner.Position + veloBot.Rotate((double)goalAngle * 0.5);

			var turns = (float)Distance.Between(owner, target) / (float)power95Per;

			if (turns <= PickUpTimer)
			{
				candidates.Add(float.MaxValue, Actions.Shoot(owner, target, power95Per));
				return;
			}
			else
			{
				foreach(var power in ShootPowers)
				{
					foreach (var goal in GoalTargets)
					{
						var velocity = Shoot.ToTarget(owner, goal, power);
						var path = BallPath.Create(owner.Position, velocity, 7, 200);
						var catchUp = path.GetCatchUps(state.Current.OtherPlayers).FirstOrDefault();
						if (catchUp == null)
						{
							candidates.Add(1000, Actions.Shoot(owner, goal, power));
							return;
						}
					}
				}
			}
		}
	}
}
