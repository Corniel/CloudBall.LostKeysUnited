using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace CloudBall.Engines.LostKeysUnited
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public struct Velocity
	{
		private const int FutureSize = 256;

		/// <summary>Represent a zero velocity (with no angle.</summary>
		public static readonly Velocity Zero = default(Velocity);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Single x;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Single y;

		public Velocity(Single x, Single y)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>Returns true if the velocity is none, otherwise false.</summary>
		public bool IsZero { get { return this.Equals(Zero); } }

		/// <summary>Gets the x component of the velocity vector.</summary>
		public Single X { get { return x; } }
		/// <summary>Gets the y component of the velocity vector.</summary>
		public Single Y { get { return y; } }

		public Distance Speed { get { return Distance.Between(default(Position), new Position(X, Y)); } }
		public double Angle { get { return IsZero ? double.NaN : Math.Atan2(X, Y); } }

		/// <summary>Gets the next velocity a ball has.</summary>
		public Velocity NextBall { get { return new Velocity(X * BallInfo.Accelaration, Y *BallInfo.Accelaration); } }

		/// <summary>Flips the velocity horizontal.</summary>
		public Velocity FlipHorizontal { get { return new Velocity(-X, Y); } }

		/// <summary>Flips the velocity vertical.</summary>
		public Velocity FlipVertical { get { return new Velocity(X, -Y); } }

		/// <summary>Gets the normalized Velocity (speed = 1).</summary>
		public Velocity Normalized { get { return new Velocity(X / (Single)Speed.GetValue(), Y / (Single)Speed.GetValue()); } }

		/// <summary>Multiplies the Velocity with a factor.</summary>
		public Velocity Add(Velocity other) { return new Velocity(X + other.X, Y + other.Y); }
		public static Velocity operator +(Velocity left, Velocity right) { return left.Add(right); }

		/// <summary>Multiplies the Velocity with a factor.</summary>
		public Velocity Multiply(float factor) { return new Velocity(X * factor, Y * factor); }
		public static Velocity operator *(Velocity velocity, float factor) { return velocity.Multiply(factor); }

		/// <summary>Casts the <see cref="Common.Vector"/> to a <see cref="CloudBall.Engines.LostKeysUnited.Velocity"/>.</summary>
		public static implicit operator Velocity(Vector vector) { return new Velocity(vector.X, vector.Y); }
		/// <summary>Casts the <see cref="CloudBall.Engines.LostKeysUnited.Velocity"/> to a <see cref="Common.Vector"/>.</summary>
		public static implicit operator Vector(Velocity velocity) { return new Vector(velocity.X, velocity.Y); }

		public override string ToString() { return string.Format(CultureInfo.CurrentCulture, "({0}, {1})", X, Y); }

		public override bool Equals(object obj) { return base.Equals(obj); }
		public override int GetHashCode() { return x.GetHashCode() ^ y.GetHashCode(); }

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private string DebuggerDisplay { get { return string.Format(CultureInfo.InvariantCulture, "Speed: {0:0.00},  angle {1:0.0}", Speed.GetValue(), Angle * 360d / Math.PI); } }

		public static Velocity FromAngle(double angle, float length = 1f)
		{
			var vector = new Velocity((Single)Math.Sin(angle) * length, (Single)Math.Cos(angle) * length);
			return vector;
		}
	}
}
