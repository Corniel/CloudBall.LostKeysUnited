using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBall.Engines.LostKeysUnited.Models
{
	// <summary>Puts the remaining players in a proper position.</summary>
	/// <remarks>
	/// 
	///  o-------------------o-------------------o
	///  |                   |                   |
	///  |   left back       |     left forward  |
	///  |                  ~|~                  |
	///  |                /  |  \                |
	///  o-------------------o-------------------o
	///  |                \  |  /                |
	///  |                  _|_                  |
	///  |   right back      |    right forward  |
	///  |                   |                   |
	///  o-------------------o-------------------o
	/// </remarks>
	public class Zones: IEnumerable<Zone>
	{
		public Zones()
		{
			Center = Game.Field.Center;
			LB = new Zone() { Name = Zone.ZoneName.LB };
			RB = new Zone() { Name = Zone.ZoneName.RB };
			LF = new Zone() { Name = Zone.ZoneName.LF };
			RF = new Zone() { Name = Zone.ZoneName.RF };
		}
		public Position Center { get; protected set; }
		public Zone LB { get; protected set; }
		public Zone RB { get; protected set; }
		public Zone LF { get; protected set; }
		public Zone RF { get; protected set; }
		public Zone BallOwnerZone { get { return this.FirstOrDefault(zone=> zone.Any(player => player.IsBallOwner)); } }

		public Zone this[Zone.ZoneName name]
		{
			get
			{
				switch (name)
				{
					case Zone.ZoneName.LB: return LB;
					case Zone.ZoneName.RB: return RB;
					case Zone.ZoneName.LF: return LF;
					case Zone.ZoneName.RF: return RF;
					default: throw new NotSupportedException();
				}
			}
		}

		/// <summary>Returns the zones that are not occupied by own players.</summary>
		public IEnumerable<Zone> NotOccupiedByOwn
		{
			get
			{
				foreach (var zone in this)
				{
					if (zone.CountOwn == 0) { yield return zone; }
				}
			}
		}

		/// <summary>Returns the zones that are occupied by one own player.</summary>
		public IEnumerable<Zone> SingleOccupiedByOwn
		{
			get
			{
				foreach (var zone in this)
				{
					if (zone.CountOwn == 1) { yield return zone; }
				}
			}
		}

		public IEnumerator<Zone> GetEnumerator()
		{
			yield return LB;
			yield return RB;
			yield return LF;
			yield return RF;
		}

		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

		public static Zones Create(GameState state)
		{
			IPoint ball = state.Current.Ball;

			var zones = new Zones();

			var centerX = (ball.X + Game.Field.CenterX) / 2f;
			var centerY = (0.2f * ball.Y + Game.Field.CenterY) / 1.2f;
			zones.Center = new Position((float)centerX, (float)centerY);

			foreach(var player in state.Current.Players)
			{
				var p = player.Position;
				var name = Zone.ZoneName.Left;
				if (p.X > centerX) { name |= Zone.ZoneName.Forward; }
				if (p.Y > centerY) { name |= Zone.ZoneName.Right; }
				zones[name].Add(player);
			}
			zones.LB.Target = new Position(centerX / 2f, centerY / 2f);
			zones.RB.Target = new Position(centerX / 2f, (Game.Field.MaximumY + centerY) / 2f);
			zones.LF.Target = new Position((Game.Field.MaximumX + centerX) / 2f, centerY / 2f);
			zones.RF.Target = new Position((Game.Field.MaximumX + centerX) / 2f, (Game.Field.MaximumY + centerY) / 2f);

			return zones;
		}

		
	}
}
