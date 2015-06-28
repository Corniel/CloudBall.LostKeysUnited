using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Models
{
	public class Zone: List<PlayerInfo>
	{
		[Flags]
		public enum ZoneName
		{
			Left = 0,
			Right = 1,
			Forward = 2,
			LB = Left,
			RB = Right,
			LF = Left | Forward,
			RF = Right | Forward,
		}

		public ZoneName Name { get; internal set; }
		public Position Target { get; internal set; }

		public IEnumerable<PlayerInfo> Own { get { return this.Where(player => player.Team == TeamType.Own); } }
		public IEnumerable<PlayerInfo> Other { get { return this.Where(player => player.Team == TeamType.Other); } }

		public int CountOwn { get { return this.Count(player => player.Team == TeamType.Own); } }
		public int CountOther { get { return this.Count(player => player.Team == TeamType.Other); } }
	}
}
