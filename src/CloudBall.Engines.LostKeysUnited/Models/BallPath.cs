using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Models
{
	/// <summary>A path that describes the movement of the ball.</summary>
	public class BallPath : List<Position>
	{
		/// <summary>Gets the acceleration of the ball (0.993025).</summary>
		public const float Accelaration = 0.9930925f;

		/// <summary>The type to ending of the path.</summary>
		public enum Ending
		{
			EndOfGame,
			GoalOwn,
			GoalOther
		}

		/// <summary>The pickup timer of the ball.</summary>
		public int PickUpTimer { get; set; }

		/// <summary>The type of ending.</summary>
		public Ending End { get; set; }

		/// <summary>The type of ending.</summary>
		public int Bounces { get; set; }

		/// <summary>Gets the effective distance.</summary>
		public Distance Distance { get { return Count < 2 ? Distance.Zero : Distance.Between(this[0], this[Count - 1]); } }

		/// <summary>Gets the catch ups for the ball path.</summary>
		public IEnumerable<CatchUp> GetCatchUps(IEnumerable<PlayerInfo> players)
		{
			var queue = new Queue<PlayerInfo>(players);

			for (var turn = PickUpTimer; turn < Count; turn++)
			{
				if (queue.Count == 0) { break; }
				for (var p = 0; p < queue.Count; p++)
				{
					var player = queue.Dequeue();
					// the player can not run yet.
					if (-player.FallenTimer > turn) { queue.Enqueue(player); continue; }

					var distanceToBall = Distance.Between(this[turn], player.Position);
					var speed = PlayerPath.GetInitialSpeed(player, this[turn]);
					var playerReach = PlayerPath.GetDistance(speed, turn + player.FallenTimer, 40);

					if (distanceToBall <= playerReach)
					{
						yield return new CatchUp()
						{
							Turn = turn,
							Player = player,
							Position = this[turn],
						};
					}
					else
					{
						queue.Enqueue(player);
					}
				}
			}
		}

		/// <summary>Creates a path based on the position and the velocity of the ball.</summary>
		public static BallPath Create(BallInfo ball, int maxLength)
		{
			return Create(ball.Position, ball.Velocity, ball.PickUpTimer, maxLength);
		}
		/// <summary>Creates a path based on the position and the velocity of the ball.</summary>
		public static BallPath Create(Position ball, Velocity velocity, int pickUpTimer, int maxLength)
		{
			var path = new BallPath() { PickUpTimer = pickUpTimer };

			var pos = ball;
			var vel = velocity;
			for (var turn = 0; turn < maxLength; turn++)
			{
				path.Add(pos);
				pos += vel;
				vel *= Accelaration;

				if (Game.Field.IsLeft(pos))
				{
					vel = vel.FlipHorizontal;
					var prev = turn == 0 ? ball : path[turn - 1];
					if (prev.Y > Goal.MinimumY && pos.Y < Goal.MaximumY &&
						prev.Y > Goal.MinimumY && pos.Y < Goal.MaximumY)
					{
						path.End = Ending.GoalOwn;
						return path;
					}
					path.Bounces++;
					var dX = Game.Field.MinimumX - pos.X;
					pos = new Position(Game.Field.MinimumX + dX, pos.Y);
				}
				else if (Game.Field.IsRight(pos))
				{
					vel = vel.FlipHorizontal;
					var prev = turn == 0 ? ball : path[turn - 1];
					if (prev.Y > Goal.MinimumY && pos.Y < Goal.MaximumY &&
						prev.Y > Goal.MinimumY && pos.Y < Goal.MaximumY)
					{
						path.End = Ending.GoalOther;
						return path;
					}
					path.Bounces++;
					var dX = Game.Field.MaximumX - pos.X;
					pos = new Position(Game.Field.MaximumX + dX, pos.Y);
				}
				if (Game.Field.IsAbove(pos))
				{
					path.Bounces++;
					vel = vel.FlipVertical;
					var dY = Game.Field.MinimumY - pos.Y;
					pos = new Position(pos.X, Game.Field.MinimumY + dY);
				}
				if (Game.Field.IsUnder(pos))
				{
					path.Bounces++;
					vel = vel.FlipVertical;
					var dY = Game.Field.MaximumY - pos.Y;
					pos = new Position(pos.X, Game.Field.MaximumY + dY);
				}
			}
			return path;
		}
	}
}
