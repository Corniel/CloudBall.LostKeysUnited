using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CloudBall.Engines.LostKeysUnited
{
	[Serializable]
	public class FieldZone : IPoint
	{
		private FieldZone() { }
		public FieldZone(int x, int y, Distance maximumShootDistance)
		{
			Center = new Position(x, y);
			DistanceToGoal = Goal.Other.GetDistance(Center);
			CheckShotOnGoal = DistanceToGoal < maximumShootDistance;
			Targets = new Dictionary<IPoint, FieldPath>();
		}

		public Position Center { get; protected set; }
		public Distance DistanceToGoal { get; protected set; }
		public bool CheckShotOnGoal { get; protected set; }

		public FieldZone[] Neighbors { get; internal set; }
		public Dictionary<IPoint, FieldPath> Targets { get; private set; }

		public IEnumerable<FieldZone> GetTargets(HashSet<FieldZone> own, HashSet<FieldZone> other)
		{
			foreach (var kvp in Targets)
			{
				var path = kvp.Value;

				if (path.IsSubsetOf(own) && !path.IsSubsetOf(other))
				{
					yield return Game.Field[kvp.Key];
				}
			}
		}

		#region IPoint

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Single IPoint.X { get { return Center.X; } }
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Single IPoint.Y { get { return Center.Y; } }

		#endregion

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendFormat("Zone[{0:0}, {1:0}]", Center.X, Center.Y);

			if (CheckShotOnGoal)
			{
				sb.Append(", GoalChance");
			}

			return sb.ToString();
		}
	}
}
