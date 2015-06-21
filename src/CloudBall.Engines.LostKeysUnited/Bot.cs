using CloudBall.Engines.LostKeysUnited.Scenarios;
using Common;
using log4net;
using System;

namespace CloudBall.Engines.LostKeysUnited
{
	[BotName("Lost Keys United 2.0")]
	public class Bot : ITeam
	{
		public static ILog Log = LogManager.GetLogger(typeof(Bot));

		/// <summary>Creates a new instance of the bot.</summary>
		public Bot()
		{
			Logging.Setup();
			Turns = new TurnInfos();
			Scenarios = new IScenario[]
			{
				Scenario.Possession,
				Scenario.Default,
			};
		}

		public TurnInfos Turns { get; private set; }

		public IScenario[] Scenarios { get; private set; }

		/// <summary>The central method that is used by the Game Engine to run the bot.</summary>
		public void Action(Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
		{
			try
			{
				Turns.Add(myTeam, enemyTeam, ball, matchInfo);

				foreach (var scenario in Scenarios)
				{
					if (scenario.Apply(Turns))
					{
						break;
					}
				}
			}
			catch (Exception x)
			{
				Log.Fatal("Uncatched:", x);
			}
		}
	}
}
