using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml.Serialization;
using NUnit.Framework;
using CloudBall.Engines.LostKeysUnited;

namespace CloudBall.Engines.LostKeysUnited.UnitTests
{
	/// <summary>Tests the angle SVO.</summary>
	[TestFixture]
	public class AngleTest
	{
		/// <summary>The test instance for most tests.</summary>
		public static readonly Angle TestStruct = Math.PI / 3d;

		#region IEquatable tests

		/// <summary>GetHash should not fail for Angle.Empty.</summary>
		[Test]
		public void GetHash_Empty_Hash()
		{
			Assert.AreEqual(0, Angle.Zero.GetHashCode());
		}

		/// <summary>GetHash should not fail for the test struct.</summary>
		[Test]
		public void GetHash_TestStruct_Hash()
		{
			Assert.AreEqual(131969591, AngleTest.TestStruct.GetHashCode());
		}

		[Test]
		public void Equals_EmptyEmpty_IsTrue()
		{
			Assert.IsTrue(Angle.Zero.Equals(Angle.Zero));
		}
	
		[Test]
		public void Equals_TestStructTestStruct_IsTrue()
		{
			Assert.IsTrue(AngleTest.TestStruct.Equals(AngleTest.TestStruct));
		}

		[Test]
		public void Equals_TestStructEmpty_IsFalse()
		{
			Assert.IsFalse(AngleTest.TestStruct.Equals(Angle.Zero));
		}

		[Test]
		public void Equals_EmptyTestStruct_IsFalse()
		{
			Assert.IsFalse(Angle.Zero.Equals(AngleTest.TestStruct));
		}

		[Test]
		public void Equals_TestStructObjectTestStruct_IsTrue()
		{
			Assert.IsTrue(AngleTest.TestStruct.Equals((object)AngleTest.TestStruct));
		}

		[Test]
		public void Equals_TestStructNull_IsFalse()
		{
			Assert.IsFalse(AngleTest.TestStruct.Equals(null));
		}

		[Test]
		public void Equals_TestStructObject_IsFalse()
		{
			Assert.IsFalse(AngleTest.TestStruct.Equals(new object()));
		}

		[Test]
		public void OperatorIs_TestStructTestStruct_IsTrue()
		{
			var l = AngleTest.TestStruct;
			var r = AngleTest.TestStruct;
			Assert.IsTrue(l == r);
		}

		[Test]
		public void OperatorIsNot_TestStructTestStruct_IsFalse()
		{
			var l = AngleTest.TestStruct;
			var r = AngleTest.TestStruct;
			Assert.IsFalse(l != r);
		}

		#endregion
		
		#region IComparable tests

		/// <summary>Orders a list of angles ascending.</summary>
		[Test]
		public void OrderBy_Angle_AreEqual()
		{
			Angle item0 = 0.12;
			Angle item1 = 0.32;
			Angle item2 = 0.80;
			Angle item3 = 1.12;

			var inp = new List<Angle>() { Angle.Zero, item3, item2, item0, item1, Angle.Zero };
			var exp = new List<Angle>() { Angle.Zero, Angle.Zero, item0, item1, item2, item3 };
			var act = inp.OrderBy(item => item).ToList();

			CollectionAssert.AreEqual(exp, act);
		}

		/// <summary>Orders a list of angles descending.</summary>
		[Test]
		public void OrderByDescending_Angle_AreEqual()
		{
			Angle item0 = 0.12;
			Angle item1 = 0.32;
			Angle item2 = 0.80;
			Angle item3 = 1.12;

			var inp = new List<Angle>() { Angle.Zero, item3, item2, item0, item1, Angle.Zero };
			var exp = new List<Angle>() { item3, item2, item1, item0, Angle.Zero, Angle.Zero };
			var act = inp.OrderByDescending(item => item).ToList();

			CollectionAssert.AreEqual(exp, act);
		}

		/// <summary>Compare with a to object casted instance should be fine.</summary>
		[Test]
		public void CompareTo_ObjectTestStruct_0()
		{
			object other = (object)TestStruct;

			var exp = 0;
			var act = TestStruct.CompareTo(other);

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void LessThan_17LT19_IsTrue()
		{
			Angle l = 0.17d;
			Angle r = 0.19d;

			Assert.IsTrue(l < r);
		}
		[Test]
		public void GreaterThan_21LT19_IsTrue()
		{
			Angle l =0.21d;
			Angle r =0.19d;

			Assert.IsTrue(l > r);
		}

		[Test]
		public void LessThanOrEqual_17LT19_IsTrue()
		{
			Angle l = 0.17d;
			Angle r = 0.19d;

			Assert.IsTrue(l <= r);
		}
		[Test]
		public void GreaterThanOrEqual_21LT19_IsTrue()
		{
			Angle l = 0.21d;
			Angle r = 0.19d;

			Assert.IsTrue(l >= r);
		}

		[Test]
		public void LessThanOrEqual_17LT17_IsTrue()
		{
			Angle l = 0.17d;
			Angle r = 0.17d;

			Assert.IsTrue(l <= r);
		}
		[Test]
		public void GreaterThanOrEqual_21LT21_IsTrue()
		{
			Angle l = 0.21d;
			Angle r = 0.21d;

			Assert.IsTrue(l >= r);
		}
		#endregion
	
		#region Properties
		#endregion
	}
}
