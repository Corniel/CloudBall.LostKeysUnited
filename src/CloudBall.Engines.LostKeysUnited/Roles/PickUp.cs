using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	public class PickUp : IRole
	{
		public PlayerInfo Apply(TurnInfos infos, IEnumerable<PlayerInfo> queue)
		{
			var info = infos.Current;
			var pickup = info.OwnPlayers.FirstOrDefault(p => p.CanPickUpBall);

			// If we cannot pick up, we can not...
			if (pickup == null) { return null; }

			// If we score and the other cannot pickup, we don't want to.
			if (infos.BallPath.Ending == BallPath.End.GoalOwn && infos.CatchUp.Result == CatchUp.ResultType.Own) { return null; }

			pickup.Apply(Actions.PickUpBall);
			return pickup;
		}
	}
}
