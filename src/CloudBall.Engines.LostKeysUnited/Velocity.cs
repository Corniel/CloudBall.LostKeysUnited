using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace CloudBall.Engines.LostKeysUnited
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public struct Velocity
	{
		/// <summary>Represent a zero velocity (with no angle.</summary>
		public static readonly Velocity Zero = default(Velocity);

		/// <summary>Creates a new velocity.</summary>
		public Velocity(Double x, Double y) : this((Single)x, (Single)y) { }

		/// <summary>Creates a new velocity.</summary>
		public Velocity(Single x, Single y)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>Returns true if the velocity is none, otherwise false.</summary>
		public bool IsZero { get { return this.Equals(Zero); } }

		/// <summary>Gets the x component of the velocity vector.</summary>
		public Single X { get { return x; } }
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Single x;

		/// <summary>Gets the y component of the velocity vector.</summary>
		public Single Y { get { return y; } }
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Single y;

		public Distance Speed { get { return new Distance(this); } }
		public Angle Angle { get { return IsZero ? Angle.NaN : Angle.Atan2(this); } }

		/// <summary>Flips the velocity horizontal.</summary>
		public Velocity FlipHorizontal { get { return new Velocity(-X, Y); } }

		/// <summary>Flips the velocity vertical.</summary>
		public Velocity FlipVertical { get { return new Velocity(X, -Y); } }

		/// <summary>Gets the normalized Velocity (speed = 1).</summary>
		public Velocity Normalized { get { return new Velocity(X / Speed, Y / Speed); } }

		/// <summary>Scales the velocity to the preferred length.</summary>
		public Velocity Scale(double length)
		{
			return new Velocity(length * X / Speed, length * Y / Speed);
		}

		/// <summary>Rotates the vector.</summary>
		public Velocity Rotate(Angle angle)
		{
			var nX = angle.Cos() * X - angle.Sin() * Y;
			var nY = angle.Sin() * X + angle.Cos() * Y;
			return new Velocity(nX, nY);
		}
		
		#region Operations

		/// <summary>Adds a Velocity to this velocity.</summary>
		public Velocity Add(Velocity other) { return new Velocity(X + other.X, Y + other.Y); }
		public static Velocity operator +(Velocity left, Velocity right) { return left.Add(right); }

		/// <summary>Subtracts a Velocity from this velocity.</summary>
		public Velocity Subtract(Velocity other) { return new Velocity(X - other.X, Y - other.Y); }
		public static Velocity operator -(Velocity left, Velocity right) { return left.Subtract(right); }

		/// <summary>Multiplies the Velocity with a factor.</summary>
		public Velocity Multiply(float factor) { return new Velocity(X * factor, Y * factor); }
		public static Velocity operator *(Velocity velocity, float factor) { return velocity.Multiply(factor); }

		#endregion

		#region Casting

		/// <summary>Casts the <see cref="Common.Vector"/> to a <see cref="CloudBall.Engines.LostKeysUnited.Velocity"/>.</summary>
		public static implicit operator Velocity(Common.Vector vector) { return new Velocity(vector.X, vector.Y); }
		
		/// <summary>Casts the <see cref="CloudBall.Engines.LostKeysUnited.Velocity"/> to a <see cref="Common.Vector"/>.</summary>
		public static implicit operator Common.Vector(Velocity velocity) { return new Common.Vector(velocity.X, velocity.Y); }

		#endregion

		public override bool Equals(object obj) { return base.Equals(obj); }
		public override int GetHashCode() { return x.GetHashCode() ^ y.GetHashCode(); }

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private string DebuggerDisplay { get { return string.Format(CultureInfo.InvariantCulture, "Speed: {0:0.00},  angle {1:0.0}°", Speed, (double)Angle * 180d / Math.PI); } }

		public override string ToString() { return string.Format("({0}, {1})", X, Y); }
	}
}
