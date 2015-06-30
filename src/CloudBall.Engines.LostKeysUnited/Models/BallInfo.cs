using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CloudBall.Engines.LostKeysUnited.Models
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public class BallInfo : IPoint
	{
		public const float MaximumPickUpDistance = 40;

		public BallInfo(Position pos, Velocity vel, int pickuptimer, PlayerInfo owner)
		{
			Position = pos;
			Velocity = vel;
			PickUpTimer = pickuptimer;
			Owner = owner;
			DistanceToOwnGoal = Goal.Own.GetDistance(pos);
			DistanceToOtherGoal = Goal.Other.GetDistance(pos);
		}

		public PlayerInfo Owner { get; private set; }
		public Position Position { get; private set; }
		public Velocity Velocity { get; private set; }
		public int PickUpTimer { get; private set; }

		public Distance DistanceToOwnGoal { get; set; }
		public Distance DistanceToOtherGoal { get; set; }

		public TeamType Team { get { return Owner == null ? TeamType.None : Owner.Team; } }
		public bool IsOwn { get { return Team == TeamType.Own; } }
		public bool IsOther { get { return Team == TeamType.Other; } }
		public bool HasOwner { get { return Owner != null; } }

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private string DebuggerDisplay
		{
			get
			{
				return String.Format
				(
					"Ball, Pos: ({0:0.0}, {1:0.0}), Speed: {2:0.0}, Angle: {3:0}, Owner: {3}",
					Position.X, Position.Y,
					Velocity.Speed,
					Velocity.Angle,
					Team
				);
			}
		}

		#region IPoint

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Single IPoint.X { get { return Position.X; } }
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Single IPoint.Y { get { return Position.Y; } }

		#endregion

		public static BallInfo Create(Common.Ball ball, PlayerInfo owner)
		{
			Guard.NotNull(ball, "ball");
			return new BallInfo(ball.Position, ball.Velocity, ball.PickUpTimer, owner);
		}
	}
}
