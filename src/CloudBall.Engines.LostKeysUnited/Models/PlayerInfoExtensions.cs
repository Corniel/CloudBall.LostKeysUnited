using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Models
{
	public static class PlayerInfoExtensions
	{
		/// <summary>Get all players excluding the current.</summary>
		public static IEnumerable<PlayerInfo> GetOther(this PlayerInfo player, IEnumerable<PlayerInfo> players)
		{
			return players.Where(p => p != player);
		}
	}
}
