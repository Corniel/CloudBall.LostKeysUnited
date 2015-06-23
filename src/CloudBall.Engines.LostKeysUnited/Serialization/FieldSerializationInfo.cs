using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CloudBall.Engines.LostKeysUnited.Serialization
{
	[Serializable]
	public class FieldSerializationInfo
	{
		public const Int16 END = Int16.MinValue;

		public byte ZoneSize { get; set; }
		public Single MaximumShootDistance { get; set; }
		public Dictionary<Int16, Int16[]>[,] Zones { get; set; }
		
		/// <summary>Load the info from stream.</summary>
		/// The default approach did not work in Arena.
		/// <remarks>
		/// <code>
		/// var serializer = new BinaryFormatter();
		/// return (FieldSerializationInfo)serializer.Deserialize(stream);
		/// </code>
		/// </remarks>
		public static FieldSerializationInfo Load(Stream stream)
		{
			var reader = new BinaryReader(stream);

			var data = new FieldSerializationInfo();
			data.ZoneSize = reader.ReadByte();
			data.MaximumShootDistance = reader.ReadSingle();
			var maxX = reader.ReadByte();
			var maxY = reader.ReadByte();

			data.Zones = new Dictionary<short, short[]>[maxX, maxY];

			for (var x = 0; x < maxX; x++)
			{
				for (var y = 0; y < maxY; y++)
				{
					var dict = new Dictionary<short, short[]>();

					while (stream.CanRead)
					{
						var key = reader.ReadInt16();
						if (key == END) { break; }

						var list = new List<Int16>();
						while(stream.CanRead)
						{
							var val = reader.ReadInt16();
							if (val == END) { break; }
							list.Add(val);
						}
						dict[key] = list.ToArray();
					}
					data.Zones[x, y] = dict;
				}
			}

			return data;
		}
		public static FieldSerializationInfo Load(string fileName) { return Load(new FileInfo(fileName)); }
		public static FieldSerializationInfo Load(FileInfo file)
		{
			using (var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
			{
				return Load(stream);
			}
		}

		/// <summary>Saves the info to stream.</summary>
		/// <remarks>
		/// The default approach did not work in Arena.
		/// <code>
		/// var serializer = new BinaryFormatter();
		/// serializer.Serialize(stream, this);
		/// </code>
		/// </remarks>

		public void Save(Stream stream)
		{
			byte maxX = (byte)Zones.GetLength(0);
			byte maxY = (byte)Zones.GetLength(1);

			var writer = new BinaryWriter(stream);
			writer.Write(ZoneSize);
			writer.Write(MaximumShootDistance);
			writer.Write(maxX);
			writer.Write(maxY);

			for (var x = 0; x < maxX; x++)
			{
				for (var y = 0; y < maxY; y++)
				{
					var dict = Zones[x, y];
					foreach (var kvp in dict)
					{
						writer.Write(kvp.Key);
						foreach(var val in kvp.Value)
						{
							writer.Write(val);
						}
						writer.Write(END);
					}
					writer.Write(END);
				}
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
