using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using CloudBall.Engines.LostKeysUnited.Conversion;

namespace CloudBall.Engines.LostKeysUnited
{
	/// <summary>Represents an angle.</summary>
	[DebuggerDisplay("{DebuggerDisplay}")]
	[Serializable]
	[TypeConverter(typeof(AngleTypeConverter))]
	public struct Angle : ISerializable, IXmlSerializable,  IFormattable, IComparable, IComparable<Angle>
	{
		/// <summary>Represents an angle of zero degrees.</summary>
		public static readonly Angle Zero = default(Angle);

		public static readonly Angle NaN = new Angle() { m_Value = double.NaN };

		/// <summary>Represents an angle of 90 degrees.</summary>
		public static readonly Angle Straight = new Angle() {m_Value = Math.PI/2d };

		/// <summary>Creates a distance with a given theta.</summary>
		public Angle(double theta) { m_Value = theta; }

		#region Properties

		/// <summary>The inner value of the angle.</summary>
		private Double m_Value;

		#endregion

		#region Operations

		/// <summary>Gets the absolute value of the angle.</summary>
		public Angle Abs() { return Math.Abs(m_Value); }

		/// <summary>Returns the sine of the angle.</summary>
		public double Sin() { return Math.Sin(m_Value); }
		
		/// <summary>Returns the cosine of the angle.</summary>
		public double Cos() { return Math.Cos(m_Value); }
		
		/// <summary>Returns the tangent of the angle.</summary>
		public double Tan() { return Math.Tan(m_Value); }

		#endregion

		#region (XML) (De)serialization

		/// <summary>Initializes a new instance of angle based on the serialization info.</summary>
		/// <param name="info">The serialization info.</param>
		/// <param name="context">The streaming context.</param>
		private Angle(SerializationInfo info, StreamingContext context)
		{
			Guard.NotNull(info, "info");
			m_Value = info.GetDouble("Value");
		}

		/// <summary>Adds the underlying property of angle to the serialization info.</summary>
		/// <param name="info">The serialization info.</param>
		/// <param name="context">The streaming context.</param>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			Guard.NotNull(info, "info");
			info.AddValue("Value", m_Value);
		}

		/// <summary>Gets the xml schema to (de) xml serialize an angle.</summary>
		/// <remarks>
		/// Returns null as no schema is required.
		/// </remarks>
		XmlSchema IXmlSerializable.GetSchema() { return null; }

		/// <summary>Reads the angle from an xml writer.</summary>
		/// <remarks>
		/// Uses the string parse function of angle.
		/// </remarks>
		/// <param name="reader">An xml reader.</param>
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			var s = reader.ReadElementString();
			var val = Parse(s, CultureInfo.InvariantCulture);
			m_Value = val.m_Value;
		}

		/// <summary>Writes the angle to an xml writer.</summary>
		/// <remarks>
		/// Uses the string representation of angle.
		/// </remarks>
		/// <param name="writer">An xml writer.</param>
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteString(ToString(CultureInfo.InvariantCulture));
		}

		#endregion
	
		#region IFormattable / ToString

		/// <summary>Returns a System.String that represents the current angle for debug purposes.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private string DebuggerDisplay
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0:0.0}°", m_Value * 180 / Math.PI);
			}
		}

		 /// <summary>Returns a System.String that represents the current angle.</summary>
		public override string ToString()
		{
			return ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Returns a formatted System.String that represents the current angle.</summary>
		/// <param name="format">
		/// The format that this describes the formatting.
		/// </param>
		public string ToString(string format)
		{
			return ToString(format, CultureInfo.CurrentCulture);
		}

		/// <summary>Returns a formatted System.String that represents the current angle.</summary>
		/// <param name="formatProvider">
		/// The format provider.
		/// </param>
		public string ToString(IFormatProvider formatProvider)
		{
			return ToString("", formatProvider);
		}

		/// <summary>Returns a formatted System.String that represents the current angle.</summary>
		/// <param name="format">
		/// The format that this describes the formatting.
		/// </param>
		/// <param name="formatProvider">
		/// The format provider.
		/// </param>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return m_Value.ToString(format, formatProvider);
		}

		#endregion
		
		#region IEquatable

		/// <summary>Returns true if this instance and the other object are equal, otherwise false.</summary>
		/// <param name="obj">An object to compair with.</param>
		public override bool Equals(object obj){ return base.Equals(obj); }

		/// <summary>Returns the hash code for this angle.</summary>
		/// <returns>
		/// A 32-bit signed integer hash code.
		/// </returns>
		public override int GetHashCode() { return m_Value.GetHashCode(); }

		/// <summary>Returns true if the left and right operand are not equal, otherwise false.</summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand</param>
		public static bool operator ==(Angle left, Angle right)
		{
			return left.Equals(right);
		}

		/// <summary>Returns true if the left and right operand are equal, otherwise false.</summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand</param>
		public static bool operator !=(Angle left, Angle right)
		{
			return !(left == right);
		}

		#endregion

		#region IComparable

		/// <summary>Compares this instance with a specified System.Object and indicates whether
		/// this instance precedes, follows, or appears in the same position in the sort
		/// order as the specified System.Object.
		/// </summary>
		/// <param name="obj">
		/// An object that evaluates to a angle.
		/// </param>
		/// <returns>
		/// A 32-bit signed integer that indicates whether this instance precedes, follows,
		/// or appears in the same position in the sort order as the value parameter.Value
		/// Condition Less than zero This instance precedes value. Zero This instance
		/// has the same position in the sort order as value. Greater than zero This
		/// instance follows value.-or- value is null.
		/// </returns>
		/// <exception cref="System.ArgumentException">
		/// value is not a angle.
		/// </exception>
		public int CompareTo(object obj)
		{
			if (obj is Angle)
			{
				return CompareTo((Angle)obj);
			}
			throw new ArgumentException("Argument must be an angle.", "obj");
		}

		/// <summary>Compares this instance with a specified angle and indicates
		/// whether this instance precedes, follows, or appears in the same position
		/// in the sort order as the specified angle.
		/// </summary>
		/// <param name="other">
		/// The angle to compare with this instance.
		/// </param>
		/// <returns>
		/// A 32-bit signed integer that indicates whether this instance precedes, follows,
		/// or appears in the same position in the sort order as the value parameter.
		/// </returns>
		public int CompareTo(Angle other) { return m_Value.CompareTo(other.m_Value); }


		/// <summary>Returns true if the left operator is less then the right operator, otherwise false.</summary>
		public static bool operator <(Angle l, Angle r) { return l.CompareTo(r) < 0; }

		/// <summary>Returns true if the left operator is greater then the right operator, otherwise false.</summary>
		public static bool operator >(Angle l, Angle r) { return l.CompareTo(r) > 0; }

		/// <summary>Returns true if the left operator is less then or equal the right operator, otherwise false.</summary>
		public static bool operator <=(Angle l, Angle r) { return l.CompareTo(r) <= 0; }

		/// <summary>Returns true if the left operator is greater then or equal the right operator, otherwise false.</summary>
		public static bool operator >=(Angle l, Angle r) { return l.CompareTo(r) >= 0; }

		#endregion
	   
		#region (Explicit) casting

		/// <summary>Casts an angle to a System.String.</summary>
		public static explicit operator string(Angle val) { return val.ToString(CultureInfo.CurrentCulture); }
		/// <summary>Casts a System.String to a angle.</summary>
		public static explicit operator Angle(string str) { return Angle.Parse(str, CultureInfo.CurrentCulture); }


		/// <summary>Casts a decimal an angle.</summary>
		public static implicit operator Angle(decimal val) { return new Angle((double)val); }
		/// <summary>Casts a decimal an angle.</summary>
		public static implicit operator Angle(double val) { return new Angle(val); }

		/// <summary>Casts an angle to a decimal.</summary>
		public static explicit operator decimal(Angle val) { return (decimal)val.m_Value; }
		/// <summary>Casts an angle to a double.</summary>
		public static explicit operator double(Angle val) { return val.m_Value; }
	   
		#endregion

		#region Factory methods

		public static Angle Asin(double value) { return Math.Asin(value); }
		public static Angle Acos(double value) { return Math.Acos(value); }
		public static Angle Atan(double value) { return Math.Atan(value); }

		public static Angle Atan2(Velocity velocity) { return Math.Atan2(velocity.Y, velocity.X); }
		public static Angle Atan2(double y, double x) { return Math.Atan2(y, x); }

		///// <summary>Gets the angle between two velocities.</summary>
		public static Angle Between(Velocity v0, Velocity v1)
		{
			var a = v0.X * v1.X + v0.Y * v1.Y;
			var b = (double)v0.Speed * (double)v1.Speed;
			return Acos(a / b);
		}

		/// <summary>Converts the string to an angle.</summary>
		/// <param name="s">
		/// A string containing an angle to convert.
		/// </param>
		/// <returns>
		/// An angle.
		/// </returns>
		/// <exception cref="System.FormatException">
		/// s is not in the correct format.
		/// </exception>
		public static Angle Parse(string s)
		{
		   return Parse(s, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the string to an angle.</summary>
		/// <param name="s">
		/// A string containing an angle to convert.
		/// </param>
		/// <param name="formatProvider">
		/// The specified format provider.
		/// </param>
		/// <returns>
		/// An angle.
		/// </returns>
		/// <exception cref="System.FormatException">
		/// s is not in the correct format.
		/// </exception>
		public static Angle Parse(string s, IFormatProvider formatProvider)
		{
			Angle val;
			if (Angle.TryParse(s, formatProvider, out val))
			{
				return val;
			}
			throw new FormatException("Not a valid angle.");
		}

		/// <summary>Converts the string to an angle.
		/// A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="s">
		/// A string containing an angle to convert.
		/// </param>
		/// <returns>
		/// The angle if the string was converted successfully, otherwise Angle.Empty.
		/// </returns>
		public static Angle TryParse(string s)
		{
			Angle val;
			if (Angle.TryParse(s, out val))
			{
				return val;
			}
			return Angle.Zero;
		}

		/// <summary>Converts the string to an angle.
		/// A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="s">
		/// A string containing an angle to convert.
		/// </param>
		/// <param name="result">
		/// The result of the parsing.
		/// </param>
		/// <returns>
		/// True if the string was converted successfully, otherwise false.
		/// </returns>
		public static bool TryParse(string s, out Angle result)
		{
			return TryParse(s, CultureInfo.CurrentCulture, out result);
		}

		/// <summary>Converts the string to an angle.
		/// A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="s">
		/// A string containing an angle to convert.
		/// </param>
		/// <param name="formatProvider">
		/// The specified format provider.
		/// </param>
		/// <param name="result">
		/// The result of the parsing.
		/// </param>
		/// <returns>
		/// True if the string was converted successfully, otherwise false.
		/// </returns>
		public static bool TryParse(string s, IFormatProvider formatProvider, out Angle result)
		{
			result = Angle.Zero;

			double d;
			if (double.TryParse(s, NumberStyles.Any, formatProvider, out d))
			{
				result = new Angle(d);
				return true;
			}
			return false;
		}

		#endregion
	}
}