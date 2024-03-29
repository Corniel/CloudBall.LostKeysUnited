﻿using System;
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

		/// <summary>Gets the distance given an initial power.</summary>
		public static Distance GetDistance(float initialSpeed, int turns)
		{
			var key = SpeedToKey(initialSpeed);

			var dis = Distances[key, Math.Min(1024, turns)];
			return dis;
		}
		private static readonly float[,] Distances;

		/// <summary>Gets the catch ups for the ball path.</summary>
		public IEnumerable<CatchUp> GetCatchUps(IEnumerable<PlayerInfo> players)
		{
			return CatchUp.GetCatchUps(this, PickUpTimer, players);
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

		/// <summary>Initializes the distances.</summary>
		static BallPath()
		{
			Distances = new float[1001, 1024];

			for (var initialSpeed = 0f; initialSpeed < 10.1f; initialSpeed += 0.1f)
			{
				var key = SpeedToKey(initialSpeed);
				var dis = initialSpeed;
				var speed = initialSpeed;

				for (var turn = 1; turn < 1024; turn++)
				{
					Distances[key, turn] = dis;
					speed *= Accelaration;
					dis += speed;
				}
			}
		}
		private static int SpeedToKey(float speed) { return (int)(speed * 10 + 0.5); }
	}
}
