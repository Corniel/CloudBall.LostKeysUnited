

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

		public bool IsLeft(IPoint point) { return point.X < MinimumX; }
		public bool IsRight(IPoint point) { return point.X > MaximumX; }
		public bool IsAbove(IPoint point){ return point.Y < MinimumY;}
		public bool IsUnder(IPoint point){ return point.Y > MaximumY;}
	}
}
