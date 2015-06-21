using CloudBall.Engines.LostKeysUnited.Roles;
using System.Collections.Generic;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	public interface IRole
	{
		/// <summary>Applies the role on one of the players and return that player.</summary>
		PlayerInfo Apply(TurnInfos infos, IEnumerable<PlayerInfo> queue);
	}
}
