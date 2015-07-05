using CloudBall.Engines.LostKeysUnited.Models;
using System;

namespace CloudBall.Engines.LostKeysUnited.ActionGeneration
{
	public class Evaluator
	{
		public static float GetPositionImprovement(IPoint source, IPoint target, int turns)
		{
			float dSource = (float)Goal.Other.GetDistance(source);
			float dTarget = (float)Goal.Other.GetDistance(target);
			var gain = dSource- dTarget;
			return gain / (float)turns;
		}
	}
}
