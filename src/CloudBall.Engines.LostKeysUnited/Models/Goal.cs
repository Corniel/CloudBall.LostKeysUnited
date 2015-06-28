
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Models
{
	/// <summary>The game has two goals. One for red, and one for blue.</summary>
	[DebuggerDisplay("{DebuggerDisplay}")]
	public class Goal
	{
		/// <summary>Gets the own goal [(0, 383); (0, 695)].</summary>
		public static readonly Goal Own = new Goal(TeamType.Own, Common.Field.MyGoal.Top, Common.Field.MyGoal.Center, Common.Field.MyGoal.Bottom);

		/// <summary>Gets the goal of the other [(1920, 383); (1920, 695)].</summary>
		public static readonly Goal Other = new Goal(TeamType.Other, Common.Field.EnemyGoal.Top, Common.Field.EnemyGoal.Center, Common.Field.EnemyGoal.Bottom);

		internal static readonly Single MinimumY = Common.Field.MyGoal.Top.Y;
		internal static readonly Single MaximumY = Common.Field.MyGoal.Bottom.Y;

		private Goal() { }
		private Goal(TeamType team, Position top, Position center, Position bottom)
		{
			Top = top;
			Center = center;
			Bottom = bottom;
		}

		public TeamType Team { get; private set; }
		public Position Center { get; private set; }
		public Position Top { get; private set; }
		public Position Bottom { get; private set; }
		public Single X { get { return Center.X; } }

		/// <summary>Gets the distance to the goal.</summary>
		/// <remarks>
		/// If above or under the goal, it takes the closed corner, otherwise
		/// it takes the X difference.
		/// </remarks>
		public Distance GetDistance(IPoint other)
		{
			var goal = Bottom;

			// above the goal.
			if (other.Y <= MinimumY)
			{
				goal = Top;
			}
			// between Top and bottom
			else if (other.Y < MaximumY)
			{
				return new Distance(X - other.X);
			}
			return Distance.Between(goal, other);
		}

		public T GetClosestBy<T>(IEnumerable<T> candidates) where T : IPoint
		{
			return candidates.OrderBy(candidate => GetDistance(candidate)).FirstOrDefault();
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private string DebuggerDisplay { get { return String.Format("{0} goal, Top: {1}, Bottom: {2}", Team, Top, Bottom); } }
	}
}
