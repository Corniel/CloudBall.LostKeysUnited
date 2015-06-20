using Common;

namespace CloudBall.Engines.LostKeysUnited
{
	/// <summary>Represents the field.</summary>
	/// <remarks>
	///  (0, 0)       (1920, 0)
	///    o-------+-------o
	///    |       |       |
	///    |       o       |
	///    |       |       |
	///    o-------+-------o
	///  (0, 1080)    (1920, 1080)
	/// 
	/// </remarks>
	public class FieldInfo
	{
		internal static readonly FieldInfo Instance = new FieldInfo();

		internal readonly float MinimumX = Field.Borders.Left.X;
		internal readonly float MinimumY = Field.Borders.Top.Y;
		internal readonly float MaximumX = Field.Borders.Right.X;
		internal readonly float MaximumY = Field.Borders.Bottom.Y;

		private FieldInfo() { }

		public Quadrant GetQuadrant(IPoint point)
		{
			var quadrant = Quadrant.None;

			if (point.X < MinimumX) { quadrant |= Quadrant.Left; }
			else if (point.X > MaximumX) { quadrant |= Quadrant.Right; }

			if (point.Y < MinimumY) { quadrant |= Quadrant.Above; }
			else if (point.Y > MaximumY) { quadrant |= Quadrant.Under; }

			if (quadrant == Quadrant.None) { quadrant = Quadrant.Field; }

			return quadrant;
		}
	}
}
