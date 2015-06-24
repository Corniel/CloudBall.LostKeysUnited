using CloudBall.Engines.LostKeysUnited.Roles;
using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	public class Possession : ScenarioBase
	{
		private static readonly Position CornerTop = Goal.Other.Top + new Velocity(0, 50);
		private static readonly Position CornerBottom = Goal.Other.Bottom - new Velocity(0, 50);

		public Possession()
		{
			Roles = new IRole[]
			{
				Role.Keeper,
				Role.Attacker,
				Role.Sweeper,
			};
		}
		protected override bool ApplyScenario(TurnInfos infos)
		{
			var info = infos.Current;

			if (!info.Ball.IsOwn) { return false; }

			var targets = new List<FieldZone>();
			var possessor = info.Ball.Owner;

			var zoneBall = Game.Field[info.Ball.Position];
			var zonesOwn = Game.Field.Select(info.OwnPlayers);
			var zonesOther = Game.Field.Select(info.OtherPlayers);

			zonesOwn.ExceptWith(zonesOther);

			if (zonesOwn.Count > 0)
			{
				targets
					.AddRange(zoneBall.GetTargets(zonesOther, zonesOther)
					.OrderBy(z => z.DistanceToOtherGoal));
			}
			if (targets.Count > 0)
			{
				var target = targets[0];
				Dequeue(possessor.Apply(Actions.Shoot(target.Center, 7.5f)));

				var pickup = Queue.OrderBy(p => Distance.Between(p, target)).FirstOrDefault();
				Dequeue(pickup.Apply(Actions.Move(target)));
			}
			else
			{
				Dequeue(possessor.Apply(Actions.ShootOnGoal(10)));
			}
			return true;
		}

		protected bool CanShootOnOpenGoal(TurnInfo info)
		{
			var possessor = info.Ball.Owner;

			if (possessor == Goal.Other.GetClosestBy(info.Players))
			{
				Dequeue(possessor.Apply(Actions.ShootOnOpenGoal()));
			}
			return false;
		}

		protected bool CanShootOnGoal(TurnInfo info)
		{
			var possessor = info.Ball.Owner;

			var path = BallPath.Create(info.Ball.Position, Goal.Other.Center, 10f, info.Turn);

			var catchUp = path.GetCatchUp(info.OtherPlayers);
			if (catchUp.Result == CatchUp.ResultType.None)
			{
				Dequeue(possessor.Apply(Actions.Shoot(Goal.Other.Center, 10f)));
				return true;
			}

			path = BallPath.Create(info.Ball.Position, CornerTop, 10f, info.Turn);
			catchUp = path.GetCatchUp(info.OtherPlayers);

			if (catchUp.Result == CatchUp.ResultType.None)
			{
				Dequeue(possessor.Apply(Actions.Shoot(CornerTop, 10f)));
				return true;
			}

			path = BallPath.Create(info.Ball.Position, CornerBottom, 10f, info.Turn);
			catchUp = path.GetCatchUp(info.OtherPlayers);

			if (catchUp.Result == CatchUp.ResultType.None)
			{
				Dequeue(possessor.Apply(Actions.Shoot(CornerBottom, 10f)));
				return true;
			}
			return false;
		}


		protected bool CanPass(TurnInfo info)
		{

			var possessor = info.Ball.Owner;
			var candiate = Goal.Other.GetClosestBy(info.OwnPlayers);

			if (candiate != possessor)
			{
				Dequeue(possessor.Apply(Actions.Shoot(candiate.Position, 7.5f)));
				return true;
			}
			return false;
		}
	}
}
