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
	/// <summary>Tests the distance SVO.</summary>
	[TestFixture]
	public class DistanceTest
	{
		/// <summary>The test instance for most tests.</summary>
		public static readonly Distance TestStruct = 666d;

		#region IEquatable tests

		/// <summary>GetHash should not fail for Distance.Empty.</summary>
		[Test]
		public void GetHash_Empty_Hash()
		{
			Assert.AreEqual(0, Distance.Zero.GetHashCode());
		}

		/// <summary>GetHash should not fail for the test struct.</summary>
		[Test]
		public void GetHash_TestStruct_Hash()
		{
			Assert.AreEqual(1092293264, DistanceTest.TestStruct.GetHashCode());
		}

		[Test]
		public void Equals_EmptyEmpty_IsTrue()
		{
			Assert.IsTrue(Distance.Zero.Equals(Distance.Zero));
		}

		[Test]
		public void Equals_TestStructTestStruct_IsTrue()
		{
			Assert.IsTrue(DistanceTest.TestStruct.Equals(DistanceTest.TestStruct));
		}

		[Test]
		public void Equals_TestStructEmpty_IsFalse()
		{
			Assert.IsFalse(DistanceTest.TestStruct.Equals(Distance.Zero));
		}

		[Test]
		public void Equals_EmptyTestStruct_IsFalse()
		{
			Assert.IsFalse(Distance.Zero.Equals(DistanceTest.TestStruct));
		}

		[Test]
		public void Equals_TestStructObjectTestStruct_IsTrue()
		{
			Assert.IsTrue(DistanceTest.TestStruct.Equals((object)DistanceTest.TestStruct));
		}

		[Test]
		public void Equals_TestStructNull_IsFalse()
		{
			Assert.IsFalse(DistanceTest.TestStruct.Equals(null));
		}

		[Test]
		public void Equals_TestStructObject_IsFalse()
		{
			Assert.IsFalse(DistanceTest.TestStruct.Equals(new object()));
		}

		[Test]
		public void OperatorIs_TestStructTestStruct_IsTrue()
		{
			var l = DistanceTest.TestStruct;
			var r = DistanceTest.TestStruct;
			Assert.IsTrue(l == r);
		}

		[Test]
		public void OperatorIsNot_TestStructTestStruct_IsFalse()
		{
			var l = DistanceTest.TestStruct;
			var r = DistanceTest.TestStruct;
			Assert.IsFalse(l != r);
		}

		#endregion
		
		#region IComparable tests

		/// <summary>Orders a list of distances ascending.</summary>
		[Test]
		public void OrderBy_Distance_AreEqual()
		{
			Distance item0 = 13245d;
			Distance item1 = 23245d;
			Distance item2 = 43245d;
			Distance item3 = 73245d;

			var inp = new List<Distance>() { Distance.Zero, item3, item2, item0, item1, Distance.Zero };
			var exp = new List<Distance>() { Distance.Zero, Distance.Zero, item0, item1, item2, item3 };
			var act = inp.OrderBy(item => item).ToList();

			CollectionAssert.AreEqual(exp, act);
		}

		/// <summary>Orders a list of distances descending.</summary>
		[Test]
		public void OrderByDescending_Distance_AreEqual()
		{
			Distance item0 = 13245d;
			Distance item1 = 23245d;
			Distance item2 = 43245d;
			Distance item3 = 73245d;

			var inp = new List<Distance>() { Distance.Zero, item3, item2, item0, item1, Distance.Zero };
			var exp = new List<Distance>() { item3, item2, item1, item0, Distance.Zero, Distance.Zero };
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
			Distance l = 17d;
			Distance r = 19d;

			Assert.IsTrue(l < r);
		}
		[Test]
		public void GreaterThan_21LT19_IsTrue()
		{
			Distance l = 21d;
			Distance r = 19d;

			Assert.IsTrue(l > r);
		}

		[Test]
		public void LessThanOrEqual_17LT19_IsTrue()
		{
			Distance l = 17d;
			Distance r = 19d;

			Assert.IsTrue(l <= r);
		}
		[Test]
		public void GreaterThanOrEqual_21LT19_IsTrue()
		{
			Distance l = 21d;
			Distance r = 19d;

			Assert.IsTrue(l >= r);
		}

		[Test]
		public void LessThanOrEqual_17LT17_IsTrue()
		{
			Distance l = 17d;
			Distance r = 17d;

			Assert.IsTrue(l <= r);
		}
		[Test]
		public void GreaterThanOrEqual_21LT21_IsTrue()
		{
			Distance l = 21d;
			Distance r = 21d;

			Assert.IsTrue(l >= r);
		}
		#endregion
		
		#region Casting tests

		[Test]
		public void Explicit_StringToDistance_AreEqual()
		{
			var exp = TestStruct;
			var act = (Distance)TestStruct.ToString();

			Assert.AreEqual(exp, act);
		}
		[Test]
		public void Explicit_DistanceToString_AreEqual()
		{
			var exp = TestStruct.ToString();
			var act = (string)TestStruct;

			Assert.AreEqual(exp, act);
		}

		#endregion

		#region Properties
		#endregion
	}
}
