using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudBall.Engines.LostKeysUnited
{
	[Serializable]
	public class FieldPath : HashSet<FieldZone>
	{
		public override string ToString()
		{
			return String.Join(",", this.Select(n => String.Format("({0},{1})", n.Center.X, n.Center.Y)));
		}

		public static FieldPath Create(FieldZone source, FieldZone target, FieldInitSettings settings, FieldInfo field)
		{
			var path = new FieldPath();

			var ball = BallPath.Create(source.Center, target, 8, 0, settings.MaximumPassDuration - 1);

			foreach (var zone in field)
			{
				var length = Math.Min(ball.Count, settings.PassDistances.Count);
				for (var i = 1; i < length; i++)
				{
					if (Distance.Between(zone, ball[i]) < settings.PassDistances[i])
					{
						path.Add(field[zone]); break;
					}
				}
			}
			return path;
		}

		public static FieldPath Create(IEnumerable<FieldZone> nodes)
		{
			var path = new FieldPath();
			foreach (var node in nodes)
			{
				path.Add(node);
			}
			return path;
		}
	}
}
