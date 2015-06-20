using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CloudBall.Engines.LostKeysUnited
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public struct Distance: IComparable, IComparable<Distance>
	{
		public static readonly Distance Zero = default(Distance);
		public static readonly Distance MaxValue = new Distance(Double.MaxValue);

		private Distance(Double distance2) { this.distance2 = distance2; }
		private Double distance2;

		/// <summary>Gets the non squared value.</summary>
		/// <remarks>
		/// This uses as Math.Sqrt and comes with a price.
		/// </remarks>
		public Double GetValue() { return Math.Sqrt(distance2); }
		public Double Squared { get { return distance2; } }

		#region IComparable

		public int CompareTo(object obj)
		{
			if (obj is Distance) { return CompareTo((Distance)obj); }
			throw new NotSupportedException();
		}
		public int CompareTo(Distance other) { return distance2.CompareTo(other.distance2); }

		public static bool operator ==(Distance l, Distance r) { return l.distance2 == r.distance2; }
		public static bool operator !=(Distance l, Distance r) { return l.distance2 != r.distance2; }

		public static bool operator >(Distance l, Distance r) { return l.distance2 > r.distance2; }
		public static bool operator <(Distance l, Distance r) { return l.distance2 < r.distance2; }
		
		public static bool operator >=(Distance l, Distance r) { return l.distance2 >= r.distance2; }
		public static bool operator <=(Distance l, Distance r) { return l.distance2 <= r.distance2; }

		#endregion

		public override bool Equals(object obj){return base.Equals(obj);}
		public override int GetHashCode() { return distance2.GetHashCode(); }

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private Double DebuggerDisplay { get { return GetValue(); } }

		/// <summary>Gets the distance between two points.</summary>
		public static Distance Between(IPoint a, IPoint b)
		{
			var dx = a.X - b.X;
			var dy = a.Y - b.Y;
			return new Distance(dx * dx + dy * dy);
		}

		public static Distance Create(double distance) { return new Distance(distance * distance); }
	}
}
