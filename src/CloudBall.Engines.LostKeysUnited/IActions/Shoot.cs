using System.Diagnostics;

namespace CloudBall.Engines.LostKeysUnited.IActions
{
	/// <summary>Shoots to a target.</summary>
	public struct Shoot : IAction
	{
		/// <summary>Creates a shoot on target.</summary>
		public Shoot(int id, IPoint target, Power power)
		{
			this.id = id;
			this.target = target;
			this.power = power;
		}

		#region Properties

		/// <summary>Gets the ID.</summary>
		public int Id { get { return id; } }
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int id;
		/// <summary>Gets the target.</summary>
		public IPoint Target { get { return target; } }
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IPoint target;

			/// <summary>Gets the power to shoot with.</summary>
		public Power Power { get { return power; } }
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Power power;

		#endregion

		/// <summary>Invokes the shoot on target.</summary>
		public void Invoke(PlayerMapping mapping) { mapping[id].ActionShoot(target.ToVector(), (float)power); }

		/// <summary>Represents the action as <see cref="System.String"/>.</summary>
		public override string ToString()
		{
			return string.Format("Player[{0}] Shoots to ({1:0}, {2:0}) with power {3:0}", id, Target.X, Target.Y, Power);
		}

		/// <summary>Shoots a ball with a certain velocity.</summary>
		/// <returns>
		/// The target where the ball will go to.
		/// </returns>
		public static Position WithVelocity(IPoint ball, Velocity velocity, Power power)
		{
			velocity = velocity.Scale((double)power * Power.PowerToSpeed);
			var target = new Position(ball.X + velocity.X, ball.Y + velocity.Y);
			return target;
		}

		/// <summary>Shoot a ball to a target.</summary>
		/// <returns>
		/// The actual velocity involved with this shot.
		/// </returns>
		public static Velocity ToTarget(IPoint ball, IPoint target, Power power)
		{
			Velocity velocity = new Velocity(target.X - ball.X, target.Y - ball.Y);
			return velocity.Scale((double)power * Power.PowerToSpeed);
		}
	}
}
