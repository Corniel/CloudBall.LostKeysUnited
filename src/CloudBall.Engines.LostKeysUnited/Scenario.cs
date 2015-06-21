﻿using CloudBall.Engines.LostKeysUnited;
using CloudBall.Engines.LostKeysUnited.Scenarios;

namespace CloudBall.Engines.LostKeysUnited
{
	/// <summary>A container class with singleton instances of different implementations.</summary>
	public static class Scenario
	{
		public static readonly IScenario Possession = new Scenarios.Possession();
		public static readonly IScenario Default = new Scenarios.Default();
	}
}
