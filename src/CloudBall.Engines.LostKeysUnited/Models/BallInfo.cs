using Common;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CloudBall.Engines.LostKeysUnited
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public class BallInfo : IPoint
	{
		public const Single PowerToSpeed = 1.2f;
		public const Single Accelaration = 0.9930925f;

		public BallInfo(Position pos, Velocity vel, int pickuptimer, Ball ball, PlayerInfo owner)
		{
			Position = pos;
			Velocity = vel;
			PickUpTimer = pickuptimer;
			Ball = ball;
			Owner = owner;
		}

		public Ball Ball { get; private set; }
		public PlayerInfo Owner { get; private set; }
		public Position Position { get; private set; }
		public Velocity Velocity { get; private set; }
		public Int32 PickUpTimer { get; private set; }

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
					Velocity.Speed.Value,
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

		public static BallInfo Create(Ball ball, PlayerInfo owner)
		{
			if (ball == null) { throw new ArgumentNullException("player"); }
			return new BallInfo(ball.Position, ball.Velocity, ball.PickUpTimer, ball, owner);
		}

		public static Velocity CreateVelocity(IPoint ball, IPoint target, float power)
		{
			return new Velocity(target.X - ball.X, target.Y - ball.Y).Scale(power * PowerToSpeed);
		}
	}
}
