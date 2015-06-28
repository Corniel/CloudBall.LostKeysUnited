using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CloudBall.Engines.LostKeysUnited.Models
{
	[DebuggerDisplay("{DebuggerDisplay}")]
	public class CatchUp
	{
		public int Turn { get; set; }
		public PlayerInfo Player { get; set; }
		public Position Position { get; set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private string DebuggerDisplay
		{
			get
			{
				return String.Format
				(
					"Turn: {0}, Player: {1}, Team: {2}, Position: ({3:0}, {4:0})",
					Turn,
					Player.Number,
					Player.Team,
					Player.Position.X,
					Player.Position.Y
				);
			}
		}

		/// <summary>Gets the catch ups for the path.</summary>
		public static IEnumerable<CatchUp> GetCatchUps(List<Position> path, int pickUpTimer, IEnumerable<PlayerInfo> players)
		{
			var queue = new Queue<PlayerInfo>(players);

			for (var turn = pickUpTimer; turn < path.Count; turn++)
			{
				if (queue.Count == 0) { break; }
				for (var p = 0; p < queue.Count; p++)
				{
					var player = queue.Dequeue();
					// the player can not run yet.
					if (-player.FallenTimer > turn) { queue.Enqueue(player); continue; }

					var distanceToBall = Distance.Between(path[turn], player.Position);
					var speed = PlayerPath.GetInitialSpeed(player, path[turn]);
					var playerReach = PlayerPath.GetDistance(speed, turn + player.FallenTimer, 40);

					if (distanceToBall <= playerReach)
					{
						yield return new CatchUp()
						{
							Turn = turn,
							Player = player,
							Position = path[turn],
						};
					}
					else
					{
						queue.Enqueue(player);
					}
				}
			}
		}

	}
}
