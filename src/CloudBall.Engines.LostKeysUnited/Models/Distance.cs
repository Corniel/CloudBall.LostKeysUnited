using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace CloudBall.Engines.LostKeysUnited
{
	[Serializable, DebuggerDisplay("{DebuggerDisplay}")]
	public struct Distance: ISerializable, IComparable, IComparable<Distance>
	{
		public static readonly Distance Zero = default(Distance);
		public static readonly Distance MaxValue = new Distance(Single.MaxValue, Mathematics.Sqrt(Single.MaxValue));

		private Distance(Single distance2, Single distance)
		{
			this.distance2 = distance2;
			this.distance = distance;
		}
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Single distance2;
		private Single distance;

		/// <summary>Gets the non squared value.</summary>
		/// <remarks>
		/// The result is cached as it is an expensive operation.
		/// </remarks>
		public Single Value
		{ 
			get{
				if(distance2 != 0 && distance == 0)
				{
					distance = Mathematics.Sqrt(distance2);
				}
				return distance;
			}
		}
		public Single Squared { get { return distance2; } }

		#region ISerializable

		private Distance(SerializationInfo info, StreamingContext context) : this(info.GetSingle("d2"), 0) { }

		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("d2", distance2);
		}

		#endregion

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

		public override string ToString() { return Value.ToString("0.#######"); }

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private Single DebuggerDisplay { get { return Value; } }

		/// <summary>Gets the distance between two points.</summary>
		public static Distance Between(IPoint a, IPoint b)
		{
			var dx = a.X - b.X;
			var dy = a.Y - b.Y;
			return new Distance(dx * dx + dy * dy, 0);
		}

		/// <summary>Creates a distance of specified length..</summary>
		public static Distance Create(Single distance) { return new Distance(distance * distance, Mathematics.Abs(distance)); }
	}
}
