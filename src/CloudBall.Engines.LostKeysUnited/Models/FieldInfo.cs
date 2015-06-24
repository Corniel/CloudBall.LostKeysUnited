using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
	public partial class FieldInfo : IEnumerable<FieldZone>
	{
		internal static FieldInfo Instance = new FieldInfo(40, Distance.Create(700));

		internal readonly float MinimumX = Field.Borders.Left.X;
		internal readonly float MinimumY = Field.Borders.Top.Y;
		internal readonly float MaximumX = Field.Borders.Right.X;
		internal readonly float MaximumY = Field.Borders.Bottom.Y;

		private FieldInfo(int zoneSize, Distance maximumShootDistance)
		{
			ZoneSize = zoneSize;
			MaximumShootDistance = maximumShootDistance;
		}

		public int Count { get { return zones.Length; } }
		public int ZoneSize { get; private set; }
		public int ZonesX { get { return zones.GetLength(0); } }
		public int ZonesY { get { return zones.GetLength(1); } }

		public Distance MaximumShootDistance { get; private set; }

		private FieldZone[,] zones;

		public FieldZone this[IPoint point]
		{
			get
			{
				var x = Math.Max(ToDimension(point.X), ZonesX - 1);
				var y = Math.Max(ToDimension(point.Y), ZonesY - 1);
				return zones[x, y];
			}
		}
		private int ToDimension(Single value) { return (int)(value / ZoneSize); }

		#region IEnumarable

		public IEnumerator<FieldZone> GetEnumerator()
		{
			for (var x = 0; x < zones.GetLength(0); x++)
			{
				for (var y = 0; y < zones.GetLength(1); y++)
				{
					yield return zones[x, y];
				}
			}
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

		#endregion

		public HashSet<FieldZone> Select(IEnumerable<PlayerInfo> players)
		{
			var set = new HashSet<FieldZone>();

			foreach (var player in players)
			{
				set.Add(this[player.Position]);
			}
			return set;
		}

		public PlayerInfo GetPlayer(FieldZone target, IEnumerable<PlayerInfo> players)
		{
			return players.FirstOrDefault(p => this[p] == target);
		}

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

		public static FieldInfo Create(FieldInitSettings settings)
		{
			settings.Calculate();
			var field = new FieldInfo(settings.ZoneSize, settings.MaximumShootDistance);
			field.AssignZones();
			field.SetNeighbors();

			foreach (var zone in field.zones)
			{
				var done = new HashSet<FieldZone>() { zone };
				var queue = new Queue<FieldZone>();
				queue.Enqueue(zone);

				while (queue.Count > 0)
				{
					field.TestTargets(zone, done, queue, settings);
				}
			}
			return field;
		}

		private void AssignZones()
		{
			var xMax = (int)(MaximumX / ZoneSize);
			var yMax = (int)(MaximumY / ZoneSize);
			zones = new FieldZone[xMax, yMax];

			for (var x = 0; x < xMax; x++)
			{
				for (var y = 0; y < yMax; y++)
				{
					var pX = ZoneSize * x + (ZoneSize >> 1);
					var pY = ZoneSize * y + (ZoneSize >> 1);
					zones[x, y] = new FieldZone(pX, pY, MaximumShootDistance);
				}
			}
		}
		private void SetNeighbors()
		{
			for (var x = 0; x < ZonesX; x++)
			{
				for (var y = 0; y < ZonesY; y++)
				{
					var n = x <= 0 ? null : zones[x - 1, y];
					var s = x >= ZonesX - 1 ? null : zones[x + 1, y];
					var w = y <= 0 ? null : zones[x, y - 1];
					var e = y >= ZonesY - 1 ? null : zones[x, y + 1];
					var collection = new List<FieldZone>() { n, e, s, w };

					zones[x, y].Neighbors = collection.Where(neighbor => neighbor != null).ToArray();
				}
			}
		}
		private void TestTargets(FieldZone source, HashSet<FieldZone> done, Queue<FieldZone> queue, FieldInitSettings settings)
		{
			var target = queue.Dequeue();
			var distance = Distance.Between(target, source);

			if (distance <= settings.MaximumPassDistance)
			{
				if (distance >= settings.MinimumPassDistance)
				{
					var sDis = source.DistanceToOtherGoal.Value;
					var tDis = target.DistanceToOtherGoal.Value;
					
					if (sDis - tDis < settings.MaximumProgressLoss)
					{
						source.Targets[target.Center] = FieldPath.Create(source, target, settings, this);
					}
					if (tDis - sDis < settings.MaximumProgressLoss)
					{
						target.Targets[source.Center] = FieldPath.Create(target, source, settings, this);
					}
				}
				foreach (var neighbor in target.Neighbors)
				{
					// already done.
					if (!done.Contains(neighbor) && !neighbor.Targets.ContainsKey(source.Center))
					{
						queue.Enqueue(neighbor);
					}
					done.Add(neighbor);
				}
			}
		}
	}
}
