using CloudBall.Engines.LostKeysUnited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	public class ShootOnGoal : IScenario
	{
		public bool Apply(TurnInfos infos)
		{
			var info = infos.Current;

			if (!info.Ball.IsOwn) { return false; }

			var closedToGoal = Goal.Other.GetClosestBy(info.Players);
			
			if (closedToGoal != info.Ball.Owner) { return false; }

			closedToGoal.Apply(Actions.ShootOnOpenGoal());

			foreach (var own in closedToGoal.GetOther(info.OwnPlayers))
			{
				own.Apply(Actions.Wait);
			}

			return true;
		}
	}
}
