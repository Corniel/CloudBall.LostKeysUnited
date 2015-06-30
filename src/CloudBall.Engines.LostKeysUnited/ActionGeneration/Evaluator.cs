using CloudBall.Engines.LostKeysUnited.Models;
using System;

namespace CloudBall.Engines.LostKeysUnited.ActionGeneration
{
	public class Evaluator
	{
		public static float GetPositionImprovement(IPoint source, IPoint target, int turns)
		{
			var gain = (double)Goal.Other.GetDistance(target) - (double)Goal.Other.GetDistance(source);
			gain /= Math.Pow(turns, 0.85);
			return (float)gain;
		}
	}
}
