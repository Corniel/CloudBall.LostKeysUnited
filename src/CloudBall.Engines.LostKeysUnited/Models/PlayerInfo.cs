
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
			this.CanBetTackled = new List<int>();
		}

		public static PlayerInfo Create(Common.Player player, Common.Ball ball, TeamType team, IEnumerable<Common.Player> other)
		{
			Guard.NotNull(player, "player");
			Guard.NotNull(ball, "ball");

			var info = new PlayerInfo()
			{
				Id = PlayerMapping.GetId(player.PlayerType, team),
				Position = player.Position,
				Velocity = player.Velocity,
				IsBallOwner = player == ball.Owner,
				CanPickUpBall = player.CanPickUpBall(ball),
				CanBetTackled = other.Select(p => PlayerMapping
					.GetId(p.PlayerType, team == TeamType.Other ? TeamType.Own: TeamType.Other)).ToList(),
				FallenTimer = player.FallenTimer,
				TackleTimer = player.TackleTimer,
			};
			return info;
		}
		public int Id { get; set; }
		public TeamType Team { get { return (TeamType)(Id >> 3); } }
		public int Number { get { return Id & 7; } }

		public Position Position { get; set; }
		public Velocity Velocity { get;  set; }
		/// <summary>The fallen timer indicates how long it will take before a player
		/// can move again.
		/// </summary>
		/// <remarks>
		/// A negative value indicates that a player can not move.
		/// </remarks>
		public int FallenTimer { get;  set; }
		public int TackleTimer { get;  set; }

		/// <summary>Returns true if the player can move.</summary>
		public bool CanMove { get { return FallenTimer == 0; } }

		public bool CanPickUpBall { get; set; }
		public bool IsBallOwner { get; set; }
		
		public bool CanTackle(PlayerInfo other)
		{
			return CanBetTackled.Contains(other.Id);
		}
		public List<int> CanBetTackled { get; set; }

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
