using CloudBall.Engines.LostKeysUnited.Roles;

namespace CloudBall.Engines.LostKeysUnited
{
	public static class Role
	{
		public static readonly IRole BallOwner = new BallOwner();
		public static readonly IRole PickUp = new PickUp();
		public static readonly IRole Tackler = new Tackler();
		public static readonly IRole Sweeper = new Sweeper();
		public static readonly IRole BallCatcher = new BallCatcher();
		public static readonly IRole Keeper = new Keeper();
		public static readonly IRole Sandwicher = new Sandwicher();
		public static readonly IRole[] ManMarkers = new IRole[] 
		{ 
			new ManMarker(0), 
			new ManMarker(1), 
			new ManMarker(2), 
			new ManMarker(3), 
			new ManMarker(4), 
		};
	}
}
