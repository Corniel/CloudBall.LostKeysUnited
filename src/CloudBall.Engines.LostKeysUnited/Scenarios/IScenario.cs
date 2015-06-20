namespace CloudBall.Engines.LostKeysUnited.Scenarios
{
	/// <summary>Represents a scenario at the game.</summary>
	public interface IScenario
	{
		/// <summary>Applies the scenario if applicable, otherwise returns false.</summary>
		bool Apply(TurnInfos infos);
	}
}
