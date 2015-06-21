using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	public class PickUpTheBall : ScenarioBase
	{
		protected override bool ApplyScenario(TurnInfos infos)
		{
			var info = infos.Current;

			var pickup = info.OwnPlayers.FirstOrDefault(p => p.CanPickUpBall);

			// If we cannot pick up, we can not...
			if (pickup == null) { return false; }

			// If we score and the other cannot pickup, we don't want to.
			if (infos.BallPath.Ending == BallPath.End.GoalOwn && infos.CatchUp.Result == CatchUp.ResultType.Own) { return false; }

			pickup.Apply(Actions.PickUpBall);
			Dequeue(pickup);
			return true;
		}
	}
}
