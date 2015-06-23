using Common;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;

namespace CloudBall.Engines.LostKeysUnited
{
	[Serializable, DebuggerDisplay("{DebuggerDisplay}")]
	public struct Position : IPoint, ISerializable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Single x;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Single y;

		public Position(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>Gets the x coordinate of the position.</summary>
		public Single X { get { return x; } }
		/// <summary>Gets the y coordinate of the position.</summary>
		public Single Y { get { return y; } }

		#region ISerializable

		private Position(SerializationInfo info, StreamingContext context)
		{
			x = info.GetSingle("x");
			y = info.GetSingle("y");
		}

		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("x", x);
			info.AddValue("y", y);
		}

		#endregion

		#region Operations

		/// <summary>Adds the velocity to the position.</summary>
		public Position Add(Velocity velocity) { return new Position(X + velocity.X, Y + velocity.Y); }
		public static Position operator +(Position position, Velocity velocity) { return position.Add(velocity); }

		/// <summary>Subtracts a position from this position.</summary>
		public Position Subtract(Velocity velocity) { return new Position(X - velocity.X, Y - velocity.Y); }
		public static Position operator -(Position position, Velocity velocity) { return position.Subtract(velocity); }


		/// <summary>Subtracts a position from this position.</summary>
		public Velocity Subtract(Position other) { return new Velocity(X - other.X, Y - other.Y); }
		public static Velocity operator -(Position left, Position right) { return left.Subtract(right); }

		#endregion

		/// <summary>Gets the squared distance between this and the other position.</summary>
		public Distance GetDistance(IPoint other) { return Distance.Between(this, other); }

		/// <summary>Casts the <see cref="Common.Vector"/> to a <see cref="CloudBall.Engines.LostKeysUnited.Position"/>.</summary>
		public static implicit operator Position(Vector vector) { return new Position(vector.X, vector.Y); }
		/// <summary>Casts the <see cref="CloudBall.Engines.LostKeysUnited.Positionr"/> to a <see cref="Common.Vector"/>.</summary>
		public static implicit operator Vector(Position position) { return new Vector(position.X, position.Y); }


		public override string ToString() { return string.Format(CultureInfo.CurrentCulture, "({0}, {1})", X, Y); }

		public override bool Equals(object obj) { return base.Equals(obj); }
		public static bool operator ==(Position l, Position r) { return l.X == r.X && l.Y == r.Y; }
		public static bool operator !=(Position l, Position r) { return !(l == r); }
		public override int GetHashCode() { return x.GetHashCode() ^ y.GetHashCode(); }

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private string DebuggerDisplay { get { return string.Format(CultureInfo.InvariantCulture, "{0:0.0}, {1:0.0}", X, Y); } }
	}
}
