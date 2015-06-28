namespace CloudBall.Engines.LostKeysUnited.IActions
{
	public struct Move : IAction
	{
		/// <summary>Creates a new move.</summary>
		public Move(int id, IPoint target)
		{
			this.id = id;
			this.target = target;
		}

		/// <summary>Gets the ID.</summary>
		public int Id { get { return id; } }
		private int id;
		/// <summary>Gets the target.</summary>
		public IPoint Target { get { return target; } }
		private IPoint target;

		/// <summary>Invokes move.</summary>
		public void Invoke(PlayerMapping mapping) { mapping[id].ActionGo(target.ToVector()); }

		/// <summary>Represents the action as <see cref="System.String"/>.</summary>
		public override string ToString()
		{
			return string.Format("Player[{0}] Move to ({1:0}, {2:0})", id, target.X, target.Y);
		}
	}
}
