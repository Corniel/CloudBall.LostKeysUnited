
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Models
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public class PlayerInfo : IPoint
	{
		public PlayerInfo()
		{
			this.CanBeTackled = new List<int>();
		}

		public static PlayerInfo Create(Common.Player player, Common.Ball ball, TeamType team, IEnumerable<Common.Player> other)
		{
			Guard.NotNull(player, "player");
			Guard.NotNull(ball, "ball");

			var tackled = other.Where(o => player.CanTackle(o)).ToList();
			if (tackled.Count > 0)
			{
			}

			var info = new PlayerInfo()
			{
				Id = PlayerMapping.GetId(player.PlayerType, team),
				Position = player.Position,
				Velocity = player.Velocity,
				IsBallOwner = player == ball.Owner,
				CanPickUpBall = player.CanPickUpBall(ball),
				CanBeTackled = tackled.Select(p => PlayerMapping.GetId(p.PlayerType, TeamType.Other)).ToList(),
				FallenTimer = player.FallenTimer,
				TackleTimer = player.TackleTimer,
			};
			info.DistanceToOwnGoal = Goal.Own.GetDistance(info);
			info.DistanceToOtherGoal = Goal.Other.GetDistance(info);
			return info;
		}
		public int Id { get; set; }
		public TeamType Team { get { return (TeamType)(Id >> 3); } }
		public int Number { get { return Id & 7; } }

		public bool IsOwn { get { return Team == TeamType.Own; } }
		public bool IsOther { get { return Team == TeamType.Other; } }

		public Position Position { get; set; }
		public Velocity Velocity { get; set; }

		public Distance DistanceToOwnGoal { get; set; }
		public Distance DistanceToOtherGoal { get; set; }

		/// <summary>The fallen timer indicates how long it will take before a player
		/// can move again.
		/// </summary>
		/// <remarks>
		/// A negative value indicates that a player can not move.
		/// </remarks>
		public int FallenTimer { get; set; }
		public int TackleTimer { get; set; }

		/// <summary>Returns true if the player can move.</summary>
		public bool CanMove { get { return FallenTimer == 0; } }

		public bool CanPickUpBall { get; set; }
		public bool IsBallOwner { get; set; }

		public bool CanTackle(PlayerInfo other)
		{
			return CanBeTackled.Contains(other.Id);
		}
		public List<int> CanBeTackled { get; set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private string DebuggerDisplay
		{
			get
			{
				return String.Format
				(
					"Team {0} [{1}], Pos: ({2:0.0}, {3:0.0}), Speed: {4:0.0}, Fallen: {5}, Tackle: {6}{7}{8}",
					Team,
					(int)Number,
					Position.X, Position.Y,
					(float)Velocity.Speed,
					FallenTimer,
					TackleTimer,
					IsBallOwner ? ", IsBallOwner" : "",
					CanPickUpBall ? ", CanPickupBall" : ""
				);
			}
		}

		#region IPoint

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Single IPoint.X { get { return Position.X; } }
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Single IPoint.Y { get { return Position.Y; } }

		#endregion
	}
}
