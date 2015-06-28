namespace CloudBall.Engines.LostKeysUnited.IActions
{
	public struct DropBall : IAction
	{
		/// <summary>Creates a new drop ball.</summary>
		public DropBall(int id) { this.id = id; }

		/// <summary>Gets the ID.</summary>
		public int Id { get { return id; } }
		private int id;

		/// <summary>Invokes drop ball.</summary>
		public void Invoke(PlayerMapping mapping) { mapping[id].ActionDropBall(); }

		/// <summary>Represents the action as <see cref="System.String"/>.</summary>
		public override string ToString()
		{
			return string.Format("Player[{0}] Drop the ball", id);
		}
	}
}
