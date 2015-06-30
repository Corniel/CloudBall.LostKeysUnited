using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBall.Engines.LostKeysUnited.IActions
{
	public class ActionCandidates: List<ActionCandidate>
	{
		public void Add(float score, IAction action)
		{
			var candidate = new ActionCandidate(score, action);
			Add(candidate);
			Sort();
		}
		
		/// <summary>Gets the best action.</summary>
		public ActionCandidate Best { get { Sort();  return this.Count == 0 ? ActionCandidate.None : this[0]; } }
	}
}
