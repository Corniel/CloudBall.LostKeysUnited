using CloudBall.Engines.LostKeysUnited.Roles;

namespace CloudBall.Engines.LostKeysUnited
{
	public static class Role
	{
		public static readonly IRole BallCatcher = new BallCatcher();
		public static readonly IRole Keeper = new Keeper();
	}
}
