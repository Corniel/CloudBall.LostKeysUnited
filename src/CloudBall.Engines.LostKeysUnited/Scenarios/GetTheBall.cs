using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	public class GetTheBall : ScenarioBase
	{
		protected override bool ApplyScenario(TurnInfos infos)
		{
			var info = infos.Current;
			if (info.Ball.IsOwn || info.OwnPlayers.All(player => player.CanPickUpBall)) { return false; }

			var getter = infos.CatchUp.GetOwn().FirstOrDefault();
			if (getter.Key == null) { return false; }

			getter.Key.Apply(Actions.Move(getter.Value.Position));
			Dequeue(getter.Key);
			return true;
		}
	}
}
