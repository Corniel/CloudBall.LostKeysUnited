namespace CloudBall.Engines.LostKeysUnited.IActions
{
	public struct PickUpBall : IAction
	{
		/// <summary>Creates a new pick up the ball.</summary>
		public PickUpBall(int id) { this.id = id; }

		/// <summary>Gets the ID.</summary>
		public int Id { get { return id; } }
		private int id;

		/// <summary>Invokes drop ball.</summary>
		public void Invoke(PlayerMapping mapping) { mapping[id].ActionPickUpBall(); }

		/// <summary>Represents the action as <see cref="System.String"/>.</summary>
		public override string ToString() { return string.Format("Player[{0}] pick up the ball", id); }
	}
}
