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

		/// <summary>Gets the ID.</summary>
		public int Id { get { return id; } }
		private int id;
		/// <summary>Gets the target.</summary>
		public IPoint Target { get { return target; } }
		private IPoint target;

			/// <summary>Gets the power to shoot with.</summary>
		public Power Power { get { return power; } }
		private Power power;

		/// <summary>Invokes the shoot on target.</summary>
		public void Invoke(PlayerMapping mapping) { mapping[id].ActionShoot(target.ToVector(), (float)power); }

		/// <summary>Represents the action as <see cref="System.String"/>.</summary>
		public override string ToString()
		{
			return string.Format("Player[{0}] Shoots to ({1:0}, {2:0}) with power {3:0.0}", id, target.X, target.Y, target);
		}
	}
}
