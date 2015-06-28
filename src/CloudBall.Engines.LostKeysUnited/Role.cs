using CloudBall.Engines.LostKeysUnited.Roles;

namespace CloudBall.Engines.LostKeysUnited
{
	public static class Role
	{
		public static readonly IRole BallOwner = new BallOwner();
		public static readonly IRole PickUp = new PickUp();
		public static readonly IRole BallCatcher = new BallCatcher();
		public static readonly IRole Keeper = new Keeper();
		//public static readonly IRole Sweeper = new Sweeper();
		//public static readonly IRole Attacker = new Attacker();
	}
}
