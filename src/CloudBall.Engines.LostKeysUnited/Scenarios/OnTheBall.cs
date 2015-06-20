namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	public class OnTheBall : IScenario
	{
		public bool Apply(TurnInfos infos)
		{
			var info = infos.Current;

			if (!info.Ball.IsOwn) { return false; }

			var shooter = info.Ball.Owner;

			shooter.Apply(Actions.ShootOnGoal());

			foreach (var player in shooter.GetOther(info.OwnPlayers))
			{
				player.Apply(Actions.Wait);
			}
			return true;
		}
	}
}
