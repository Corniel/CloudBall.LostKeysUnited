using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	public class BallCatcher : IRole
	{
		public PlayerInfo Apply(TurnInfos infos, IEnumerable<PlayerInfo> queue)
		{
			var info = infos.Current;
			if (info.Ball.IsOwn || info.OwnPlayers.Any(player => player.CanPickUpBall)) { return null; }

			var kvp = infos.CatchUp.GetOwn().FirstOrDefault();
			if (kvp.Key == null) { return null; }

			var catcher = kvp.Key;
			var target = kvp.Value.Position;
			return catcher.Apply(Actions.Move(target));
		}
	}
}
