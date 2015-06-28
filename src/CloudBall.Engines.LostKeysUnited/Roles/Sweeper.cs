using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	public class Sweeper // : IRole
	{
		//public PlayerInfo Apply(TurnInfos infos, IEnumerable<PlayerInfo> queue)
		//{
		//	var info = infos.Current;
		//	var target = Game.Field.Select(info.OtherPlayers)
		//		.OrderBy(z => z.DistanceToOwnGoal)
		//		.FirstOrDefault();
			
		//	var attacker = Game.Field.GetPlayer(target, info.OtherPlayers);

		//	var source = Game.Field.Select(queue)
		//		.Where(z => z.DistanceToOwnGoal < target.DistanceToOwnGoal)
		//		.OrderBy(z => Distance.Between(z, target))
		//		.FirstOrDefault();

		//	var sweeper = Game.Field.GetPlayer(source, queue);

		//	if (sweeper != null)
		//	{
		//		if (sweeper.CanTackle(attacker))
		//		{
		//			return sweeper.Apply(Actions.Tacle(attacker));
		//		}
		//		else
		//		{
		//			return sweeper.Apply(Actions.Move(target));
		//		}
		//	}
		//	return null;
		//}
	}
}
