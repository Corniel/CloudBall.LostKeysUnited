namespace CloudBall.Engines.LostKeysUnited.IActions
{
	/// <summary>No action.</summary>
	public struct NoAction : IAction
	{
		public int Id { get { return 0; } }
		public void Invoke(PlayerMapping mapping) { }

		/// <summary>Represents the action as <see cref="System.String"/>.</summary>
		public override string ToString() { return "No action"; }
	}
}
