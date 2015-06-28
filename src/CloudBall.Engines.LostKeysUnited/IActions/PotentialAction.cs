using System;
using System.Diagnostics;
using System.Globalization;

namespace CloudBall.Engines.LostKeysUnited.IActions
{
	public struct PotentialAction : IComparable, IComparable<PotentialAction>
	{
		public static readonly PotentialAction None = new PotentialAction(float.MinValue, Actions.None);

		public PotentialAction(float score, IAction action)
		{
			this.score = score;
			this.action = action;
		}
		
		public float Score { get { return score; } }
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float score;

		public IAction Action { get { return action; } }
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IAction action;

		/// <summary>Returns true if this is the none action.</summary>
		public bool IsNone { get { return None.Equals(this); } }

		public int CompareTo(PotentialAction other)
		{
			return other.Score.CompareTo(Score);
		}
		public int CompareTo(object obj)
		{
			return CompareTo((PotentialAction)obj);
		}

		public override string ToString()
		{
			if (IsNone) { return "-.--- None"; }
			return String.Format(CultureInfo.InvariantCulture, "{0:0.000} {1}", Score, Action);
		}
	}
}
