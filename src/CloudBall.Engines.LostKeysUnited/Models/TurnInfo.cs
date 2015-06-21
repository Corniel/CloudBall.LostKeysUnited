using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public class TurnInfo
	{
		public TurnInfo()
		{
			this.Players = new List<PlayerInfo>();
		}

		public int Turn { get; set; }
		public int OwnScore { get; set; }
		public bool OwnTeamScored { get; set; }
		public int OtherScore { get; set; }
		public bool OtherTeamScored { get; set; }

		public BallInfo Ball { get; set; }

		public List<PlayerInfo> Players { get; set; }
		public IEnumerable<PlayerInfo> OwnPlayers { get { return Players.Where(player => player.Team == TeamType.Own); } }
		public IEnumerable<PlayerInfo> OtherPlayers { get { return Players.Where(player => player.Team == TeamType.Other); } }

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private string DebuggerDisplay
		{
			get
			{
				return String.Format
				(
					"Turn: {0}, {1}{2}-{3}{4}",
					this.Turn,
					this.OwnScore,
					this.OwnTeamScored ? "*" : "",
					this.OtherScore,
					this.OtherTeamScored ? "*" : ""
				);
			}
		}
	}
}
