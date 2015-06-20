using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace CloudBall.Engines.LostKeysUnited
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public struct TurnPosition
	{
		public static readonly TurnPosition Unknown = new TurnPosition(Game.LastTurn + 1, default(Position));

		private Int32 turn;
		private Position pos;

		public TurnPosition(Int32 turn, Position pos)
		{
			this.turn = turn;
			this.pos = pos;
		}

		public bool IsUnknown { get { return this.Equals(Unknown); } }

		public Int32 Turn { get { return turn; } }
		public Position Position { get { return pos; } }

		public override string ToString() { return DebuggerDisplay; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private string DebuggerDisplay { get { return string.Format(CultureInfo.InvariantCulture, "Turn: {0}, ({1:0.0}, {2:0.0})", turn, pos.X, pos.Y); } }
	}
}
