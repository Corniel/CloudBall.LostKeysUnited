﻿using CloudBall.Engines.LostKeysUnited.Models;
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
			this.Stopwatch = Stopwatch.StartNew();
			this.Players = new List<PlayerInfo>();
		}
		protected Stopwatch Stopwatch { get; set; }
		public int Turn { get; set; }
		public int OwnScore { get; set; }
		public bool OwnTeamScored { get; set; }
		public int OtherScore { get; set; }
		public bool OtherTeamScored { get; set; }

		public BallInfo Ball { get; set; }

		public List<PlayerInfo> Players { get; set; }
		public IEnumerable<PlayerInfo> OwnPlayers { get { return Players.Where(player => player.Team == TeamType.Own); } }
		public IEnumerable<PlayerInfo> OtherPlayers { get { return Players.Where(player => player.Team == TeamType.Other); } }

		public void Finish()
		{
			Stopwatch.Stop();
		}

		/// <summary>Get a message for the error logging.</summary>
		public string GetErrorMessage() { return DebuggerDisplay; }

		public override string ToString() { return DebuggerDisplay; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private string DebuggerDisplay
		{
			get
			{
				return String.Format
				(
					"Turn: {0}, {1}{2}-{3}{4}, Duration: {5:0.000} millisecond",
					this.Turn,
					this.OwnScore,
					this.OwnTeamScored ? "*" : "",
					this.OtherScore,
					this.OtherTeamScored ? "*" : "",
					this.Stopwatch.Elapsed.TotalMilliseconds
				);
			}
		}
	}
}
