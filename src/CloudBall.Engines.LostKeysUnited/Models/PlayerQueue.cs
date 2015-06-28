using CloudBall.Engines.LostKeysUnited.IActions;
using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.Models
{
	/// <summary>Represents the queue of available players.</summary>
	public class PlayerQueue : List<PlayerInfo>
	{
		/// <summary>Constructor.</summary>
		public PlayerQueue()
		{
			Actions = new List<IAction>();
		}

		/// <summary>Constructor.</summary>
		public PlayerQueue(params PlayerInfo[] players)
			: this(Guard.NotNull(players, "players").AsEnumerable()) { }

		/// <summary>Constructor.</summary>
		public PlayerQueue(IEnumerable<PlayerInfo> players): this()
		{
			AddRange(Guard.NotNull(players, "players").Where(player => player.CanMove));
		}

		/// <summary>De-queues the player attached to action.</summary>
		public bool Dequeue(IAction action)
		{
			var player = this.FirstOrDefault(p => p.Id == action.Id);
			
			if (player != null)
			{
				Actions.Add(action);
				Remove(player);
			}
			return player != null;
		}

		/// <summary>Gets the actions.</summary>
		public List<IAction> Actions { get; protected set; }
	}
}
