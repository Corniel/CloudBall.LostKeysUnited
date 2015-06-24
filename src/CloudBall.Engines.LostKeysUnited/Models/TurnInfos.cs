using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited
{
	public class TurnInfos : Dictionary<Int32, TurnInfo>
	{
		public TurnInfos()
		{
			this.BallPath = new BallPath();
		}

		public BallPath BallPath { get; protected set; }
		public CatchUp CatchUp { get; protected set; }

		public TurnInfo Current { get; protected set; }
		public int Turn { get { return Current == null ? int.MinValue : Current.Turn; } }

		public void Add(TurnInfo info)
		{
			if (Turn < info.Turn) { Current = info; }
			this[info.Turn] = info;
			BallPath.Update(info);
			CatchUp = BallPath.GetCatchUp(info.Players);
		}

		public void Add(Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
		{
			var info = new TurnInfo()
			{
				Turn = matchInfo.CurrentTimeStep,

				OwnScore = matchInfo.MyScore,
				OwnTeamScored = matchInfo.MyTeamScored,

				OtherScore = matchInfo.EnemyScore,
				OtherTeamScored = matchInfo.EnemyTeamScored,
			};
			foreach (var p in myTeam.Players)
			{
				info.Players.Add(PlayerInfo.Create(p, ball, TeamType.Own));
			}
			foreach (var p in enemyTeam.Players)
			{
				info.Players.Add(PlayerInfo.Create(p, ball, TeamType.Other));
			}
			var owner = ball.Owner == null ? null : info.Players.FirstOrDefault(p => p.Player == ball.Owner);
			info.Ball = BallInfo.Create(ball, owner);
			Add(info);
		}

		/// <summary>Get a message for the error logging.</summary>
		public string GetErrorMessage() { return Current.GetErrorMessage(); }
	}
}
