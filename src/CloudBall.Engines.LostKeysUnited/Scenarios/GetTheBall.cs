using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	public class GetTheBall : IScenario
	{
		public bool Apply(TurnInfos infos)
		{
			var info = infos.Current;
			if (info.Ball.IsOwn || info.OwnPlayers.All(player => player.CanPickUpBall)) { return false; }

			var getter = infos.CatchUp.GetOwn().FirstOrDefault();
			if (getter.Key == null) { return false; }

			getter.Key.Apply(Actions.Move(getter.Value.Position));

			// TODO: don't spoil.
			foreach (var player in getter.Key.GetOther(info.OwnPlayers))
			{
				player.Apply(Actions.Wait);
			}

			return true;
		}
	}
}
