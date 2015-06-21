namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	public class OnTheBall : ScenarioBase
	{
		protected override bool ApplyScenario(TurnInfos infos)
		{
			var info = infos.Current;

			if (!info.Ball.IsOwn) { return false; }

			var shooter = info.Ball.Owner;

			shooter.Apply(Actions.ShootOnGoal());
			Dequeue(shooter);
			return true;
		}
	}
}
