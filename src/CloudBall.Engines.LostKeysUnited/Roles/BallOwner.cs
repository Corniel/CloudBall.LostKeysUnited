using CloudBall.Engines.LostKeysUnited.ActionGeneration;
using CloudBall.Engines.LostKeysUnited.IActions;
using CloudBall.Engines.LostKeysUnited.Models;
using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Roles
{
	public class BallOwner : IRole
	{
		public BallOwner()
		{
			Generators = new List<IActionGenerator>()
			{
				Generator.ShootOnGoal,
				Generator.Dribble,
				Generator.Pass,
			};
		}
		public List<IActionGenerator> Generators { get; set; }

		public bool Apply(GameState state, PlayerQueue queue)
		{
			var owner = queue.FirstOrDefault(player => player.IsBallOwner);

			if (owner == null) { return false; }

			var candidates = new ActionCandidates();
			// Just in case, if we don't find anything better.
			candidates.Add(-20000, Actions.ShootOnGoal(owner, Power.Maximum));
			foreach (var generator in Generators)
			{
				generator.Generate(owner, state, candidates);
			}
			return queue.Dequeue(candidates.Best.Action);
		}
	}
}
