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
		public const int MaximumPathLength = 200;

		public static readonly Angle[] Angles = new Angle[]
		{
			00d / 8d * Math.PI,
			01d / 8d * Math.PI,
			02d / 8d * Math.PI,
			03d / 8d * Math.PI,
			04d / 8d * Math.PI,
			05d / 8d * Math.PI,
			06d / 8d * Math.PI,
			07d / 8d * Math.PI,
			08d / 8d * Math.PI,
			09d / 8d * Math.PI,
			10d / 8d * Math.PI,
			11d / 8d * Math.PI,
			12d / 8d * Math.PI,
			13d / 8d * Math.PI,
			14d / 8d * Math.PI,
			15d / 8d * Math.PI,
		};
		private static readonly Velocity Default = new Velocity(1, 0).Scale(PassPower.Speed);
		public static readonly Velocity[] Velocities = new Velocity[]
		{
			Default.Rotate(Angles[00]),
			Default.Rotate(Angles[01]),
			Default.Rotate(Angles[02]),
			Default.Rotate(Angles[03]),
			Default.Rotate(Angles[04]),
			Default.Rotate(Angles[05]),
			Default.Rotate(Angles[06]),
			Default.Rotate(Angles[07]),
			Default.Rotate(Angles[08]),
			Default.Rotate(Angles[09]),
			Default.Rotate(Angles[10]),
			Default.Rotate(Angles[11]),
			Default.Rotate(Angles[12]),
			Default.Rotate(Angles[13]),
			Default.Rotate(Angles[14]),
			Default.Rotate(Angles[15]),
		};

		public void Generate(PlayerInfo owner, GameState state, ActionCandidates candidates)
		{
			if (!owner.IsBallOwner) { return; }

			foreach (var pass in Velocities)
			{
				var path = BallPath.Create(owner.Position, pass, PickUpTimer, MaximumPathLength);

				// We don't want to risk passes ending up in our own goal.
				if (path.End != BallPath.Ending.GoalOwn)
				{
					var catchUp = path.GetCatchUps(state.Current.Players).FirstOrDefault();

					// safe to pass.
					if (catchUp == null || catchUp.Player.IsOwn)
					{
						var length = catchUp == null ? path.Count : catchUp.Turn;
						var target = catchUp == null ? path.Last() : catchUp.Position;

						if (Distance.Between(target, owner) > MinimumPassDistance)
						{
							candidates.Add(
								Evaluator.GetPositionImprovement(owner, target, length),
								Actions.Shoot(owner, owner.Position + pass, PassPower));
						}
					}
				}
			}
		}
	}
}
