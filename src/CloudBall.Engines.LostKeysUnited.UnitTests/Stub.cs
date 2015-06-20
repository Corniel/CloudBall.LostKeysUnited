using CloudBall.Engines.LostKeysUnited;
using Common;
using GameCommon;

namespace CloudBall.Engines.LostKeysUnited.UnitTests
{
	public static class Stub
	{
		public static BallInfo NewBall(float pX, float pY, float vX, float vY, PlayerInfo owner = null, int pickuptimer = 0)
		{
			var pos = new Vector(pX, pY);
			var vel = new Vector(vX, vY);
			return new BallInfo(pos, vel, pickuptimer, null, owner);
		}

		public static PlayerInfo NewPlayer(float pX, float pY, float vX, float vY, int number, TeamType team, bool canpickupball = false, int fallentimer = 0, int tackletimer = 0)
		{
			var pos = new Position(pX, pY);
			var vel = new Velocity(vX, vY);
			return new PlayerInfo(number, team, pos, vel, canpickupball, null, fallentimer, tackletimer);
		}
	}
}
