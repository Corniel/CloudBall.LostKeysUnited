using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CloudBall.Engines.LostKeysUnited
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public class Goal
	{
		/// <summary>Gets the own goal.</summary>
		public static readonly Goal Own = new Goal(TeamType.Own, Field.MyGoal.Top, Field.MyGoal.Center, Field.MyGoal.Bottom);
		
		/// <summary>Gets the goal of the other.</summary>
		public static readonly Goal Other = new Goal(TeamType.Other, Field.EnemyGoal.Top, Field.EnemyGoal.Center, Field.EnemyGoal.Bottom);

		internal static readonly Single MinimumY = Field.MyGoal.Top.Y;
		internal static readonly Single MaximumY = Field.MyGoal.Bottom.Y;

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

		public T GetClosestBy<T>(IEnumerable<T> candidates) where T : IPoint
		{
			T closed = default(T);
			var distance = Distance.MaxValue;

			foreach (var candidate in candidates)
			{
				var goal = Top;

				// under the goal.
				if (candidate.X <= Bottom.X)
				{
					goal = Bottom;
				}
				// between Top and bottom
				else if(candidate.X > Top.X)
				{
					goal = new Position(candidate.X, Top.Y);
				}
				var test = Distance.Between(goal, candidate);

				if (test < distance)
				{
					distance = test;
					closed = candidate;
				}
			}
			return closed;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private string DebuggerDisplay { get { return String.Format("{0} goal, Top: {0}, Bottom: {1}", Team, Top, Bottom); } }
	}
}
