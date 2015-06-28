using CloudBall.Engines.LostKeysUnited.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited
{
	public class GameState : Dictionary<Int32, TurnInfo>
	{
		public BallPath Path { get; set; }
		public List<CatchUp> CatchUps { get; set; }

		public TurnInfo Current { get; protected set; }
		public int Turn { get { return Current == null ? int.MinValue : Current.Turn; } }

		public void Add(TurnInfo info)
		{
			if (Turn < info.Turn) { Current = info; }
			this[info.Turn] = info;
			Path = BallPath.Create(info.Ball, Game.LastTurn - info.Turn);
			CatchUps = Path.GetCatchUps(info.Players).ToList();
		}

		public void Add(Common.Team myTeam, Common.Team enemyTeam, Common.Ball ball, Common.MatchInfo matchInfo)
		{
			var info = new TurnInfo()
			{
				Turn = matchInfo.CurrentTimeStep,

				OwnScore = matchInfo.MyScore,
				OwnTeamScored = matchInfo.MyTeamScored,

				OtherScore = matchInfo.EnemyScore,
				OtherTeamScored = matchInfo.EnemyTeamScored,
			};
			foreach (var player in myTeam.Players)
			{
				info.Players.Add(PlayerInfo.Create(player, ball, TeamType.Own, enemyTeam.Players));
			}
			foreach (var player in enemyTeam.Players)
			{
				info.Players.Add(PlayerInfo.Create(player, ball, TeamType.Other, myTeam.Players));
			}

			var owner = ball.Owner == null ? null : info.Players.Single(p => p.IsBallOwner);
			info.Ball = BallInfo.Create(ball, owner);
			Add(info);
		}

		/// <summary>Get a message for the error logging.</summary>
		public string GetErrorMessage() { return Current.GetErrorMessage(); }
	}
}
