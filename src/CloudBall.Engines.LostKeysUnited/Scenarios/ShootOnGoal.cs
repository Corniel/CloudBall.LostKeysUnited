namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	public class ShootOnGoal : ScenarioBase
	{
		protected override bool ApplyScenario(TurnInfos infos)
		{
			var info = infos.Current;

			if (!info.Ball.IsOwn) { return false; }

			var closedToGoal = Goal.Other.GetClosestBy(info.Players);

			if (closedToGoal != info.Ball.Owner) { return false; }

			closedToGoal.Apply(Actions.ShootOnOpenGoal());
			Dequeue(closedToGoal);
			return true;
		}
	}
}
