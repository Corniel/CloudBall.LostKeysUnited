using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBall.Engines.LostKeysUnited
{
	public static class PlayerInfoExtensions
	{
		public static IEnumerable<PlayerInfo> GetOther(this  PlayerInfo player, IEnumerable<PlayerInfo> players)
		{
			return players.Where(p => p != player);
		}
	}
}
