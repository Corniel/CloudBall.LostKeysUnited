using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CloudBall.Engines.LostKeysUnited.Models
{
	public class PlayerPath : List<Position>
	{
		public const float MaximumVelocity = 3;
		public const Single AccelerationFactor = 0.071773462f;
		public const Single SlowDownFactor = 0.93303299f;

		/// <summary>Gets the distance.</summary>
		public Distance Distance { get { return Count < 2 ? Distance.Zero : Distance.Between(this[0], Last); } }

		/// <summary>Gets the last position of the path.</summary>
		public Position Last { get { return Count == 0 ? default(Position) : this[Count - 1]; } }

		/// <summary>Returns true if the specified target was reached, otherwise false./summary>
		public bool ReachedTarget { get; protected set; }

		/// <summary>Gets the distance given an initial speed.</summary>
		public static Distance GetDistance(float initialSpeed, int turns, float tolerance)
		{
			if (float.IsNaN(initialSpeed)) { initialSpeed = 0; }
			var key = (int)(initialSpeed * 100f + 300.49f);

			var dis = Distances[key, Math.Min(255, turns)];

			if (turns > 255)
			{
				dis += (turns - 255) * MaximumVelocity;
			}
			return dis + tolerance;
			
		}
		private static readonly float[,] Distances;
		/// <summary>Gets the initial speed relative to the target to reach.</summary>
		public static float GetInitialSpeed(PlayerInfo player, IPoint target)
		{
			return GetInitialSpeed(player.Velocity, player, target);
		}
		/// <summary>Gets the initial speed relative to the target to reach.</summary>
		public static float GetInitialSpeed(Velocity velocity, IPoint source, IPoint target)
		{
			return (float)NewVelocity(velocity, source, target).Speed;
		}

		/// <summary>Gets new velocity.</summary>
		public static Velocity NewVelocity(Velocity velocity, IPoint source, IPoint target)
		{
			var dX = target.X - source.X;
			var dY = target.Y - source.Y;
			var direction = new Velocity(dX, dY).Scale(MaximumVelocity * AccelerationFactor);
			return (velocity + direction) * SlowDownFactor;
		}

		/// <summary>Gets the catch ups for the ball path.</summary>
		public IEnumerable<CatchUp> GetCatchUps(IEnumerable<PlayerInfo> players)
		{
			return CatchUp.GetCatchUps(this, 0, players);
		}

		/// <summary>Creates a player path.</summary>
		public static PlayerPath Create(PlayerInfo player, IPoint target, int maxLength, Distance tolerance)
		{
			return Create(player.Position, player.Velocity, target, player.FallenTimer, maxLength, tolerance);
		}
		/// <summary>Creates a player path.</summary>
		public static PlayerPath Create(Position player, Velocity velocity, IPoint target, int fallenTimer, int maxLength, Distance tolerance)
		{
			var path = new PlayerPath();

			for(var fallen = fallenTimer; fallen < 0;fallen++)
			{
				path.Add(player);
			}

			var pos = player;
			var vel = velocity;

			for (var turn = -fallenTimer; turn < maxLength; turn++)
			{
				if (Distance.Between(pos, target) < tolerance)
				{
					path.ReachedTarget = true;
					break;
				}
				path.Add(pos);
				pos += vel;
				vel = NewVelocity(vel, pos, target);

			}
			return path;
		}

		/// <summary>Initializes the distances.</summary>
		static PlayerPath()
		{
			Distances = new float[601, 256];

			for (var key = 0; key <= 600; key++)
			{
				if (key == 600)
				{
				}
				var speed = (key - 300f) / 100f;
				var dis = speed;

				for (var turn = 1; turn < 256; turn++)
				{
					Distances[key, turn] = dis;
					speed += MaximumVelocity * AccelerationFactor;
					speed *= SlowDownFactor;
					dis += speed;
				}
			}
		}
	}
}
