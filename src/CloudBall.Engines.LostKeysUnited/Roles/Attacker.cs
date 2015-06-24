using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	public class Attacker: IRole
	{
		private static readonly Distance FreeSpace = Distance.Create(100);
		private static readonly Distance DistanceFromGoal = Distance.Create(150);

		public PlayerInfo Apply(TurnInfos infos, IEnumerable<PlayerInfo> queue)
		{
			var info = infos.Current;
			var source = Game.Field.Select(queue)
				.OrderBy(z => z.DistanceToOtherGoal)
				.FirstOrDefault();
			if (source == null) { return null; }

			var attacker = Game.Field.GetPlayer(source, queue);

			var target = Game.Field
				.Where(z => 
					z.DistanceToOtherGoal >= DistanceFromGoal &&
					z.Center.Y < (1920 - 100) && 
					z.DistanceToOtherGoal <= source.DistanceToOtherGoal &&
					info.OtherPlayers.All(other => Distance.Between(attacker, other) > FreeSpace))
				.OrderBy(z => z.DistanceToOtherGoal)
				.FirstOrDefault();

			if (target != null)
			{
				return attacker.Apply(Actions.Move(target));
			}
			return null;
		}
	}
}
