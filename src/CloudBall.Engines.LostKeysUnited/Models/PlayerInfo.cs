using Common;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CloudBall.Engines.LostKeysUnited
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public class PlayerInfo : IPoint
	{
		public const Single MaximumVelocity = 3;
		public const Single AccelerationFactor = 0.071773462f;
		public const Single SlowDownFactor = 0.93303299f;

		public PlayerInfo(
			int number,
			TeamType team,
			Position pos,
			Velocity vel,
			bool canpickupball,
			Player player,
			int fallentimer = 0,
			int tackletimer = 0)
		{
			Id = number | ((Int32)team << 3);
			Position = pos;
			Velocity = vel;
			CanPickUpBall = canpickupball;
			Player = player;
			FallenTimer = fallentimer;
			TackleTimer = tackletimer;
		}

		public static PlayerInfo Create(Player player, Ball ball, TeamType team)
		{
			if (player == null) { throw new ArgumentNullException("player"); }

			var number = (Int32)player.PlayerType;
			return new PlayerInfo(
				number,
				team,
				player.Position,
				player.Velocity,
				player.CanPickUpBall(ball),
				player,
				player.FallenTimer,
				player.TackleTimer);
		}
		public Int32 Id { get; private set; }
		public TeamType Team { get { return (TeamType)(Id >> 3); } }
		public PlayerType Number { get { return (PlayerType)(Id & 7); } }

		internal Player Player { get; private set; }
		public Position Position { get; private set; }
		public Velocity Velocity { get; private set; }
		public bool CanPickUpBall { get; private set; }
		public Int32 FallenTimer { get; private set; }
		public Int32 TackleTimer { get; private set; }

		public void Apply(IAction action) { action.Invoke(this); }

		public Velocity GetVelocity(IPoint destination)
		{
			var dX = Position.X - destination.X;
			var dY = Position.Y - destination.Y;
			var direction = new Velocity(dX, dY).Normalized * (MaximumVelocity * AccelerationFactor);
			return (Velocity + direction) * SlowDownFactor;
		}

		public Single GetTraveled(Int32 turns)
		{
			return GetTraveled(Velocity, turns);
		}

		public static Single GetTraveled(Velocity velocity, Int32 turns)
		{
			var speed = (int)(0.5 + velocity.Speed.Squared * 10);
			return traveled[speed, turns];
		}
		private static readonly Single[,] traveled;

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private string DebuggerDisplay
		{
			get
			{
				return String.Format
				(
					"Team {0} [{1}], Pos: ({2:0.0}, {3:0.0}), Speed: {4:0.0}, Angle: {5:0}",
					Team,
					(int)Number,
					Position.X, Position.Y,
					Velocity.Speed.GetValue(),
					Velocity.Angle
				);
			}
		}

		#region IPoint

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Single IPoint.X { get { return Position.X; } }
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Single IPoint.Y { get { return Position.Y; } }

		#endregion

		static PlayerInfo()
		{
			// the diagonal = 2203 so the maximum time is 735.
			traveled = new Single[128, 1024];

			for (var speed = 0; speed <= 90; speed++)
			{
				var spe = Mathematics.Sqrt(speed / 10f);
				var dis = 0f;
				for (var turn = 0; turn < 1024; turn++)
				{
					traveled[speed, turn] = dis;
					spe = (spe + 1 * MaximumVelocity * AccelerationFactor) * SlowDownFactor;
					dis += spe;
				}
			}
		}
	}
}
