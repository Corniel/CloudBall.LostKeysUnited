using CloudBall.Engines.LostKeysUnited.Models;
using System;

namespace CloudBall.Engines.LostKeysUnited.ActionGeneration
{
	public class Evaluator
	{
		public static float GetPositionImprovement(IPoint source, IPoint target, int turns)
		{
			var gain = 2000f - Goal.Other.GetDistance(target);
			return (float)gain;
		}
	}
}
