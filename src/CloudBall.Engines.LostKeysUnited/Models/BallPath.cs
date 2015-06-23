using Common;
using System;
using System.Collections.Generic;

namespace CloudBall.Engines.LostKeysUnited
{
	public class BallPath : Dictionary<Int32, Position>
	{
		public static readonly Single MaxPickUpDistance = Constants.BallMaxPickUpDistance * 0.9f;

		public enum End
		{
			Unkown,
			EndOfGame,
			GoalOwn,
			GoalOther
		}

		public Int32 Turn { get; protected set; }
		public End Ending { get; protected set; }

		public void Update(TurnInfo info)
		{
			Turn = info.Turn;
			var ball = info.Ball;
			var pos = ball.Position;
			var vel = ball.Owner == null ? ball.Velocity : Velocity.Zero;

			this[info.Turn] = pos;

			Update(pos, vel);
		}

		private void Update(Position pos, Velocity vel, int lastTurn = int.MaxValue)
		{
			// the ball rolls, we can calculate the path.
			for (var turn = Turn + 1; turn <= Math.Min(Game.LastTurn, lastTurn); turn++)
			{
				pos = pos + vel;
				vel = vel.NextBall;

				var quadrant = Game.Field.GetQuadrant(pos);

				if (quadrant.HasFlag(Quadrant.Left))
				{
					vel = vel.FlipHorizontal;
					var prev = this[turn - 1];
					if (prev.Y > Goal.MinimumY && pos.Y < Goal.MaximumY &&
						prev.Y > Goal.MinimumY && pos.Y < Goal.MaximumY)
					{
						Ending = End.GoalOther;
						return;
					}
					var dX = Game.Field.MinimumX - pos.X;
					pos = new Position(Game.Field.MinimumX + dX, pos.Y);
				}
				else if (quadrant.HasFlag(Quadrant.Right))
				{
					vel = vel.FlipHorizontal;
					var prev = this[turn - 1];
					if (prev.Y > Goal.MinimumY && pos.Y < Goal.MaximumY &&
						prev.Y > Goal.MinimumY && pos.Y < Goal.MaximumY)
					{
						Ending = End.GoalOwn;
						return;
					}
					var dX = Game.Field.MaximumX - pos.X;
					pos = new Position(Game.Field.MaximumX + dX, pos.Y);
				}
				if (quadrant.HasFlag(Quadrant.Above))
				{
					vel = vel.FlipVertical;
					var dY = Game.Field.MinimumY - pos.Y;
					pos = new Position(pos.X, Game.Field.MinimumY + dY);
				}
				else if (quadrant.HasFlag(Quadrant.Under))
				{
					vel = vel.FlipVertical;
					var dY = Game.Field.MaximumY - pos.Y;
					pos = new Position(pos.X, Game.Field.MaximumY + dY);
				}
				this[turn] = pos;
			}
			Ending = End.EndOfGame;
		}

		/// <summary>Gets the turn and position of the moment the player catches up with the ball.</summary>
		/// <remarks>
		/// the difference should be smaller than the pick up tolerance:
		/// 
		/// d[ball] < d[player] + tolerance =>
		/// 
		/// d[ball]^2 < (d[player] + tolerance)^2
		/// 
		/// Test for the last, as it way faster than using Math.Sqrt().
		/// </remarks>
		public TurnPosition GetCatchUp(PlayerInfo player)
		{
			if (player.CanPickUpBall) { return new TurnPosition(Turn, player.Position); }

			var passed = Turn + player.FallenTimer;

			for (var turn = Turn + 1 + player.FallenTimer; turn <= Game.LastTurn; turn++)
			{
				Position ball;

				if (TryGetValue(turn, out ball))
				{
					var distanceToBall = Distance.Between(player, ball);

					var traveled = player.GetTraveled(turn - passed) + MaxPickUpDistance;

					if (distanceToBall.Squared < traveled * traveled)
					{
						return new TurnPosition(turn, this[turn]);
					}
				}
			}
			return TurnPosition.Unknown;
		}

		public CatchUp GetCatchUp(IEnumerable<PlayerInfo> players)
		{
			var result = new CatchUp();

			foreach (var player in players)
			{
				result[player] = GetCatchUp(player);
			}
			return result;
		}

		public static BallPath Create(Position ball, IPoint target, Single power, int turn, int lastTurn = int.MaxValue)
		{
			var path = new BallPath() { Turn = turn };
			path[turn] = ball;

			Velocity velocity = BallInfo.CreateVelocity(ball, target, power);

			path.Update(ball, velocity, lastTurn);

			return path;
		}
	}
}
