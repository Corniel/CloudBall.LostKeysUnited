namespace CloudBall.Engines.LostKeysUnited.IActions
{
	public struct Tackle : IAction
	{
		/// <summary>Creates a new drop ball.</summary>
		public Tackle(int id, int target)
		{
			this.id = id;
			this.target = target;
		}

		/// <summary>Gets the ID.</summary>
		public int Id { get { return id; } }
		private int id;

		/// <summary>Gets the target ID.</summary>
		public int Target { get { return target; } }
		private int target;

		/// <summary>Invokes drop ball.</summary>
		public void Invoke(PlayerMapping mapping) { mapping[id].ActionTackle(mapping[target]); }

		/// <summary>Represents the action as <see cref="System.String"/>.</summary>
		public override string ToString()
		{
			return string.Format("Player[{0}] Tackle player {1}", id, target);
		}
	}
}
