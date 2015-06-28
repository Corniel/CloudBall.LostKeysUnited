

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
	public partial class FieldInfo
	{
		internal static FieldInfo Instance = new FieldInfo();

		public readonly float MinimumX = Common.Field.Borders.Left.X;
		public readonly float CenterX = Common.Field.Borders.Center.X;
		public readonly float MaximumX = Common.Field.Borders.Right.X;

		public readonly float MinimumY = Common.Field.Borders.Top.Y;
		public readonly float CenterY = Common.Field.Borders.Center.Y;
		public readonly float MaximumY = Common.Field.Borders.Bottom.Y;

		
		private FieldInfo() { }

		public readonly Position Center = new Position(Common.Field.Borders.Center.X, Common.Field.Borders.Center.Y);

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
