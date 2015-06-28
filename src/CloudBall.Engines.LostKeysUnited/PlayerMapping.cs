using CloudBall.Engines.LostKeysUnited.IActions;
using CloudBall.Engines.LostKeysUnited.Models;

using System.Collections.Generic;

namespace CloudBall.Engines.LostKeysUnited
{
	/// <summary>Maps the players with player info ID's.</summary>
	/// <remarks>
	/// By doing this, we only need the original model when applying the actions.
	/// </remarks>
	public class PlayerMapping : Dictionary<int, Common.Player>
	{
		/// <summary>Applies the actions to the players.</summary>
		public void Apply(IEnumerable<IAction> actions)
		{
			Guard.NotNull(actions, "actions");

			foreach (var action in actions)
			{
				action.Invoke(this);
			}
		}

		/// <summary>Gets the id for a player.</summary>
		public static int GetId(Common.PlayerType number, TeamType team) { return (int)number | ((int)team << 3); }

		/// <summary>Creates a mapping for the players.</summary>
		public static PlayerMapping CreateForOwn(IEnumerable<Common.Player> own, IEnumerable<Common.Player> other)
		{
			Guard.NotNull(own, "own");
			Guard.NotNull(other, "other");

			var mapping = new PlayerMapping();
			
			foreach (var player in own)
			{
				mapping.Add(GetId(player.PlayerType, TeamType.Own), player);
			}
			foreach (var player in other)
			{
				mapping.Add(GetId(player.PlayerType, TeamType.Other), player);
			}
			return mapping;
		}
	}
}
