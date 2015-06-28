using CloudBall.Engines.LostKeysUnited.IActions;
using CloudBall.Engines.LostKeysUnited.Models;

namespace CloudBall.Engines.LostKeysUnited
{
	/// <summary>Centralized factory for creation actions.</summary>
	public static class Actions
	{
		public static readonly IAction None = new NoAction();
		public static IAction DropBall(PlayerInfo player) { return new DropBall(player.Id); }
		public static IAction Move(PlayerInfo player, IPoint target) { return new Move(player.Id, target); }
		public static IAction PickUpBall(PlayerInfo player) { return new PickUpBall(player.Id); }
		public static Shoot Shoot(PlayerInfo player, IPoint target, Power power) { return new Shoot(player.Id, target, power); }
		public static IAction ShootOnGoal(PlayerInfo player, Power power) { return new ShootOnGoal(player.Id, power); }
		public static IAction Tacle(PlayerInfo player, PlayerInfo target) { return new Tackle(player.Id, target.Id); }
		public static IAction Wait(PlayerInfo player) { return new Wait(player.Id); } 
	}
}
