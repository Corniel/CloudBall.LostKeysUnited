using CloudBall.Engines.LostKeysUnited.Models;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	public class ManMarker : IRole
	{
		public ManMarker(int rank) { Rank = rank; }

		/// <summary>The rank of the man to mark.</summary>
		public int Rank { get; private set; }

		public bool Apply(GameState state, PlayerQueue queue)
		{
			if (state.Current.Ball.IsOther)
			{
				var freeMan = state.Current.Ball.Owner.GetOther(state.Current.OtherPlayers)
					.OrderBy(player => player.DistanceToOwnGoal)
					.Skip(Rank).FirstOrDefault();

				var path = PlayerPath.Create(freeMan, Goal.Own.Center, 400, 40f);

				var manMarker = path.GetCatchUps(queue).FirstOrDefault();

				if (manMarker != null)
				{
					return queue.Dequeue(Actions.Move(manMarker.Player, manMarker.Position));
				}
			}
			return false;
		}
	}
}
