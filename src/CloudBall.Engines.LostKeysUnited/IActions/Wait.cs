namespace CloudBall.Engines.LostKeysUnited.IActions
{
	public struct Wait : IAction
	{
		/// <summary>Creates a new wait.</summary>
		public Wait(int id) { this.id = id; }

		/// <summary>Gets the ID.</summary>
		public int Id { get { return id; } }
		private int id;

		/// <summary>Invokes wait.</summary>
		/// <remarks>
		/// Calling sometimes ended up having null reference exceptions.
		/// </remarks>
		public void Invoke(PlayerMapping mapping)
		{
			try
			{
				mapping[id].ActionWait();
			}
			catch (System.NullReferenceException) { }
		}

		/// <summary>Represents the action as <see cref="System.String"/>.</summary>
		public override string ToString() { return string.Format("Player[{0}] Wait", id); }
	}
}
