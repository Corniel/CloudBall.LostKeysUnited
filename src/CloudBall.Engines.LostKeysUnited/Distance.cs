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
	/// <summary>Represents a distance.</summary>
	/// <remarks>
	/// Because most distances are requested by doing the Pythagorean theorem, the
	/// value is mostly provided as squared distance. Therefore it stored that way.
	/// Only if the none squared value is required, it is calculated.
	/// </remarks>
	[DebuggerDisplay("{DebuggerDisplay}")]
	[TypeConverter(typeof(DistanceTypeConverter))]
	public struct Distance : ISerializable, IXmlSerializable, IFormattable, IComparable, IComparable<Distance>
	{
		/// <summary>Represents an empty/not set distance.</summary>
		public static readonly Distance Zero = default(Distance);

		/// <summary>Creates the distance/speed/length for a velocity.</summary>
		public Distance(Velocity velocity) { m_Value = velocity.X * velocity.X + velocity.Y * velocity.Y; }
		/// <summary>Creates a distance with a given length.</summary>
		public Distance(double value) { m_Value = value * value; }

		#region Properties

		/// <summary>The inner value of the distance.</summary>
		private Double m_Value;

		#endregion
	
		#region (XML) (De)serialization

		/// <summary>Initializes a new instance of distance based on the serialization info.</summary>
		/// <param name="info">The serialization info.</param>
		/// <param name="context">The streaming context.</param>
		private Distance(SerializationInfo info, StreamingContext context)
		{
			Guard.NotNull(info, "info");
			m_Value = info.GetDouble("Value");
		}

		/// <summary>Adds the underlying property of distance to the serialization info.</summary>
		/// <param name="info">The serialization info.</param>
		/// <param name="context">The streaming context.</param>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			Guard.NotNull(info, "info");
			info.AddValue("Value", m_Value);
		}

		/// <summary>Gets the xml schema to (de) xml serialize a distance.</summary>
		/// <remarks>
		/// Returns null as no schema is required.
		/// </remarks>
		XmlSchema IXmlSerializable.GetSchema() { return null; }

		/// <summary>Reads the distance from an xml writer.</summary>
		/// <remarks>
		/// Uses the string parse function of distance.
		/// </remarks>
		/// <param name="reader">An xml reader.</param>
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			var s = reader.ReadElementString();
			var val = Parse(s, CultureInfo.InvariantCulture);
			m_Value = val.m_Value;
		}

		/// <summary>Writes the distance to an xml writer.</summary>
		/// <remarks>
		/// Uses the string representation of distance.
		/// </remarks>
		/// <param name="writer">An xml writer.</param>
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteString(ToString(CultureInfo.InvariantCulture));
		}

		#endregion

		#region IFormattable / ToString

		/// <summary>Returns a System.String that represents the current distance for debug purposes.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private Double DebuggerDisplay { get { return ToDouble(); } }

		 /// <summary>Returns a System.String that represents the current distance.</summary>
		public override string ToString()
		{
			return ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Returns a formatted System.String that represents the current distance.</summary>
		/// <param name="format">
		/// The format that this describes the formatting.
		/// </param>
		public string ToString(string format)
		{
			return ToString(format, CultureInfo.CurrentCulture);
		}

		/// <summary>Returns a formatted System.String that represents the current distance.</summary>
		/// <param name="formatProvider">
		/// The format provider.
		/// </param>
		public string ToString(IFormatProvider formatProvider)
		{
			return ToString("", formatProvider);
		}

		/// <summary>Returns a formatted System.String that represents the current distance.</summary>
		/// <param name="format">
		/// The format that this describes the formatting.
		/// </param>
		/// <param name="formatProvider">
		/// The format provider.
		/// </param>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return ToDouble().ToString(format, formatProvider);
		}

		#endregion
		
		#region IEquatable

		/// <summary>Returns true if this instance and the other object are equal, otherwise false.</summary>
		/// <param name="obj">An object to compair with.</param>
		public override bool Equals(object obj){ return base.Equals(obj); }

		/// <summary>Returns the hash code for this distance.</summary>
		/// <returns>
		/// A 32-bit signed integer hash code.
		/// </returns>
		public override int GetHashCode() { return m_Value.GetHashCode(); }

		/// <summary>Returns true if the left and right operand are not equal, otherwise false.</summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand</param>
		public static bool operator ==(Distance left, Distance right)
		{
			return left.Equals(right);
		}

		/// <summary>Returns true if the left and right operand are equal, otherwise false.</summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand</param>
		public static bool operator !=(Distance left, Distance right)
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
		/// An object that evaluates to a distance.
		/// </param>
		/// <returns>
		/// A 32-bit signed integer that indicates whether this instance precedes, follows,
		/// or appears in the same position in the sort order as the value parameter.Value
		/// Condition Less than zero This instance precedes value. Zero This instance
		/// has the same position in the sort order as value. Greater than zero This
		/// instance follows value.-or- value is null.
		/// </returns>
		/// <exception cref="System.ArgumentException">
		/// value is not a distance.
		/// </exception>
		public int CompareTo(object obj)
		{
			if (obj is Distance)
			{
				return CompareTo((Distance)obj);
			}
			throw new ArgumentException(string.Format("The argument must be a distance."), "obj");
		}

		/// <summary>Compares this instance with a specified distance and indicates
		/// whether this instance precedes, follows, or appears in the same position
		/// in the sort order as the specified distance.
		/// </summary>
		/// <param name="other">
		/// The distance to compare with this instance.
		/// </param>
		/// <returns>
		/// A 32-bit signed integer that indicates whether this instance precedes, follows,
		/// or appears in the same position in the sort order as the value parameter.
		/// </returns>
		public int CompareTo(Distance other) { return m_Value.CompareTo(other.m_Value); }


		/// <summary>Returns true if the left operator is less then the right operator, otherwise false.</summary>
		public static bool operator <(Distance l, Distance r) { return l.CompareTo(r) < 0; }

		/// <summary>Returns true if the left operator is greater then the right operator, otherwise false.</summary>
		public static bool operator >(Distance l, Distance r) { return l.CompareTo(r) > 0; }

		/// <summary>Returns true if the left operator is less then or equal the right operator, otherwise false.</summary>
		public static bool operator <=(Distance l, Distance r) { return l.CompareTo(r) <= 0; }

		/// <summary>Returns true if the left operator is greater then or equal the right operator, otherwise false.</summary>
		public static bool operator >=(Distance l, Distance r) { return l.CompareTo(r) >= 0; }

		#endregion
	   
		#region (Explicit) casting

		/// <summary>Converts a (squared) distance to a double.</summary>
		private Double ToDouble() { return Math.Sqrt(m_Value); }

		/// <summary>Casts a distance to a System.String.</summary>
		public static explicit operator string(Distance val) { return val.ToString(CultureInfo.CurrentCulture); }
		/// <summary>Casts a System.String to a distance.</summary>
		public static explicit operator Distance(string str) { return Distance.Parse(str, CultureInfo.CurrentCulture); }


		/// <summary>Casts a decimal a distance.</summary>
		public static implicit operator Distance(decimal val) { return new Distance((double)val); }
		/// <summary>Casts a decimal a distance.</summary>
		public static implicit operator Distance(double val) { return new Distance(val); }

		/// <summary>Casts a distance to a decimal.</summary>
		public static implicit operator decimal(Distance val) { return (decimal)val.ToDouble(); }
		/// <summary>Casts a distance to a double.</summary>
		public static implicit operator double(Distance val) { return val.ToDouble(); }
	   
		#endregion

		#region Factory methods

		/// <summary>Gets the distance between two points.</summary>
		public static Distance Between(IPoint a, IPoint b)
		{
			var dx = a.X - b.X;
			var dy = a.Y - b.Y;
			return new Distance() { m_Value = dx * dx + dy * dy };
		}

		/// <summary>Converts the string to a distance.</summary>
		/// <param name="s">
		/// A string containing a distance to convert.
		/// </param>
		/// <returns>
		/// A distance.
		/// </returns>
		/// <exception cref="System.FormatException">
		/// s is not in the correct format.
		/// </exception>
		public static Distance Parse(string s)
		{
		   return Parse(s, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the string to a distance.</summary>
		/// <param name="s">
		/// A string containing a distance to convert.
		/// </param>
		/// <param name="formatProvider">
		/// The specified format provider.
		/// </param>
		/// <returns>
		/// A distance.
		/// </returns>
		/// <exception cref="System.FormatException">
		/// s is not in the correct format.
		/// </exception>
		public static Distance Parse(string s, IFormatProvider formatProvider)
		{
			Distance val;
			if (Distance.TryParse(s, formatProvider, out val))
			{
				return val;
			}
			throw new FormatException("not a valid distance.");
		}

		/// <summary>Converts the string to a distance.
		/// A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="s">
		/// A string containing a distance to convert.
		/// </param>
		/// <returns>
		/// The distance if the string was converted successfully, otherwise Distance.Empty.
		/// </returns>
		public static Distance TryParse(string s)
		{
			Distance val;
			if (Distance.TryParse(s, out val))
			{
				return val;
			}
			return Distance.Zero;
		}

		/// <summary>Converts the string to a distance.
		/// A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="s">
		/// A string containing a distance to convert.
		/// </param>
		/// <param name="result">
		/// The result of the parsing.
		/// </param>
		/// <returns>
		/// True if the string was converted successfully, otherwise false.
		/// </returns>
		public static bool TryParse(string s, out Distance result)
		{
			return TryParse(s, CultureInfo.CurrentCulture, out result);
		}

		/// <summary>Converts the string to a distance.
		/// A return value indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="s">
		/// A string containing a distance to convert.
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
		public static bool TryParse(string s, IFormatProvider formatProvider, out Distance result)
		{
			result = Distance.Zero;
			if (string.IsNullOrEmpty(s))
			{
				return true;
			}
			double d;
			if (double.TryParse(s, NumberStyles.Any, formatProvider, out d))
			{
				result = new Distance(d);
				return true;
			}
			return false;
		}

		#endregion
	 }
}