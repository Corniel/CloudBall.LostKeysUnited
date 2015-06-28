using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CloudBall.Engines.LostKeysUnited.Models
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public class CatchUp
	{
		public int Turn { get; set; }
		public PlayerInfo Player { get; set; }
		public Position Position { get; set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private string DebuggerDisplay
		{
			get
			{
				return String.Format
				(
					"Turn: {0}, Player: {1}, Team: {2}, Position: ({3:0}, {4:0})",
					Turn,
					Player.Number,
					Player.Team,
					Player.Position.X,
					Player.Position.Y
				);
			}
		}
	}
}
