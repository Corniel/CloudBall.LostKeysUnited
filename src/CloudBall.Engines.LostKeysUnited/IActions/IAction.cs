namespace CloudBall.Engines.LostKeysUnited.IActions
{
	/// <summary>Represents a (player) action.</summary>
	public interface IAction
	{
		/// <summary>Gets the ID of the involved player.</summary>
		int Id { get; }

		/// <summary>Invokes the action.</summary>
		void Invoke(PlayerMapping mapping);
	}
}
