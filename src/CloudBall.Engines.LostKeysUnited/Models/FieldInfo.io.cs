using CloudBall.Engines.LostKeysUnited.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited
{
	public partial class FieldInfo
	{
		public static FieldInfo Load(Stream stream)
		{
			var data = FieldSerializationInfo.Load(stream);

			var field = new FieldInfo(data.ZoneSize, Distance.Create(data.MaximumShootDistance));
			field.AssignZones();
			field.SetNeighbors();

			for (var x = 0; x < field.ZonesX; x++)
			{
				for (var y = 0; y < field.ZonesY; y++)
				{
					var dict = data.Zones[x, y];
					foreach (var kvp in dict)
					{
						var zone = field.ReadPosition(kvp.Key);
						FieldPath path = FieldPath.Create(kvp.Value.Select(v => field.ReadPosition(v)));
						field.zones[x, y].Targets[zone] = path;
					}
				}
			}
			return field;
		}

		public void Save(Stream stream)
		{
			var data = new FieldSerializationInfo();
			data.ZoneSize = (byte)ZoneSize;
			data.MaximumShootDistance = MaximumShootDistance.Value;
			data.Zones = new Dictionary<short, short[]>[ZonesX, ZonesY];
			
			for (var x = 0; x < ZonesX; x++)
			{
				for (var y = 0; y < ZonesY; y++)
				{
					data.Zones[x, y] = new Dictionary<short, short[]>();
					var zone = zones[x, y];

					foreach (var kvp in zone.Targets)
					{
						var target = kvp.Key;
						data.Zones[x, y][ToInt16(target)] = kvp.Value.Select(n => ToInt16(n)).ToArray();
					}
				}
			}
			data.Save(stream);
		}

		private FieldZone ReadPosition(Int16 s)
		{
			var x = s & 255;
			var y = s >> 8;
			return zones[x, y];
		}
		private Int16 ToInt16(IPoint point)
		{
			var x = ToDimension(point.X);
			var y = ToDimension(point.Y);

			Int16 s = (Int16)((x & 255) | (y << 8));
			return s;
		}
		private static bool IsLast(Int16 s) { return (s & 128) == 128; }

		public static FieldInfo Load(string fileName) { return Load(new FileInfo(fileName)); }
		public static FieldInfo Load(FileInfo file)
		{
			using (var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
			{
				return Load(stream);
			}
		}
		public void Save(string fileName) { Save(new FileInfo(fileName)); }
		public void Save(FileInfo file)
		{
			using (var stream = new FileStream(file.FullName, FileMode.Create, FileAccess.Write))
			{
				Save(stream);
			}
		}
	}
}
