namespace CloudBall.Engines.LostKeysUnited.IActions
{
	/// <summary>Shoots on goal.</summary>
	public struct ShootOnGoal : IAction
	{
		/// <summary>Creates a new shoot on goal.</summary>
		public ShootOnGoal(int id, Power power)
		{
			this.id = id; 
			this.power = power;
		}

		/// <summary>Gets the ID.</summary>
		public int Id { get { return id; } }
		private int id;

		/// <summary>Gets the power to shoot with.</summary>
		public Power Power { get { return power; } }
		private Power power;

		/// <summary>Invokes shoot on goal.</summary>
		public void Invoke(PlayerMapping mapping) { mapping[id].ActionShootGoal((float)power); }

		/// <summary>Represents the action as <see cref="System.String"/>.</summary>
		public override string ToString()
		{
			return string.Format("Player[{0}] Shoot on goal with power {1:0.0}", id, power);
		}
	}
}
