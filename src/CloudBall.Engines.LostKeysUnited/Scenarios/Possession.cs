using CloudBall.Engines.LostKeysUnited.Roles;
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
				Role.Keeper
			};
		}
		protected override bool ApplyScenario(TurnInfos infos)
		{
			var info = infos.Current;

			if (!info.Ball.IsOwn) { return false; }

			if (CanShootOnOpenGoal(info)) { }
			else if (CanShootOnGoal(info)) { }
			else if (CanPass(info)) { }
			else
			{
				var possessor = info.Ball.Owner;

				if (Goal.Other.GetDistance(possessor) > Distance.Create(1000))
				{
					var y = possessor.Position.Y < Goal.Other.Center.Y ? Game.Field.MinimumY : Game.Field.MinimumY;
					var target = new Position(Goal.Other.X, y);
					Dequeue(possessor.Apply(Actions.Shoot(target, 10f)));
				}
				else
				{
					Dequeue(possessor.Apply(Actions.ShootOnGoal(10)));
				}
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
