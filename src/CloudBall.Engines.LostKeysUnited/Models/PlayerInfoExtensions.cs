using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Models
{
	public static class PlayerInfoExtensions
	{
		/// <summary>Gets the closed by candidate for a target.</summary>
		public static T GetClosedBy<T>(this IPoint target, IEnumerable<T> candidates) where T : IPoint
		{
			return candidates.OrderBy(candidate => Distance.Between(target, candidate)).FirstOrDefault();
		}

		/// <summary>Get all players excluding the current.</summary>
		public static IEnumerable<PlayerInfo> GetOther(this PlayerInfo player, IEnumerable<PlayerInfo> players)
		{
			return players.Where(p => p != player);
		}
	}
}
