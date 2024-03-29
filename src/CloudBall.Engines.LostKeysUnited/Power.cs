﻿using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CloudBall.Engines.LostKeysUnited
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public struct Power
	{
		public const float PowerToSpeed = 1.2f;
		public static readonly Power Minimum = new Power(0);
		public static readonly Power Maximum = new Power(10);

		/// <summary>Creates a power with a value between 0 and 10.</summary>
		public Power(float value)
		{
			m_Value = Math.Max(0, Math.Min(value, 10));
		}

		#region Property

		private float m_Value;

		/// <summary>Gets the initial speed associated with the power.</summary>
		public float Speed { get { return m_Value * PowerToSpeed; } }

		#endregion



		/// <summary>Multiplies the power.</summary>
		public Power Multiply(float factor) { return m_Value * factor; }
		public static Power operator *(Power power, float factor) { return power.Multiply(factor); }

		#region (Explicit) casting

		/// <summary>Casts a double to power.</summary>
		public static implicit operator Power(float val) { return new Power(val); }
		/// <summary>Casts power to a double.</summary>
		public static explicit operator float(Power val) { return val.m_Value; }

		#endregion
		
		public override string ToString() { return m_Value.ToString(); }

		/// <summary>Returns a System.String that represents the current distance for debug purposes.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private float DebuggerDisplay { get { return m_Value; } }
	}

}
