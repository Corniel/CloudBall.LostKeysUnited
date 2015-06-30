using System;
using System.Diagnostics;
using System.Globalization;

namespace CloudBall.Engines.LostKeysUnited.IActions
{
	public struct ActionCandidate : IComparable, IComparable<ActionCandidate>
	{
		public static readonly ActionCandidate None = new ActionCandidate(float.MinValue, Actions.None);

		public ActionCandidate(float score, IAction action)
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

		public int CompareTo(ActionCandidate other)
		{
			return other.Score.CompareTo(Score);
		}
		public int CompareTo(object obj)
		{
			return CompareTo((ActionCandidate)obj);
		}

		public override string ToString()
		{
			if (IsNone) { return "-.--- None"; }
			return String.Format(CultureInfo.InvariantCulture, "{0:0.000} {1}", Score, Action);
		}
	}
}
