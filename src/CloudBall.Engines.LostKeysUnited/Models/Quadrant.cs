using System;

namespace CloudBall.Engines.LostKeysUnited
{
	/// <summary>The different quadrants that you can have at the field.</summary>
	/// <remarks>
	///            Above
	///      o-------+-------o
	///      |       |       |
	/// Left |       o       | Right
	///      |       |       |
	///      o-------+-------o
	///            Under
		/// </remarks>
	[Flags]
	public enum Quadrant
	{
		None = 0,
		Field = 1,
		Above = 2,
		Under = 4,
		Left = 8,
		Right = 16,
	}
}
