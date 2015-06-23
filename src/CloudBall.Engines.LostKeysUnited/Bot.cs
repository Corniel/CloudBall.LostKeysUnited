using CloudBall.Engines.LostKeysUnited.Scenarios;
using Common;
using log4net;
using System;
using System.Diagnostics;
using System.IO;

namespace CloudBall.Engines.LostKeysUnited
{
	[BotName("Lost Keys United 4.0")]
	public class Bot : ITeam
	{
		/// <summary>Gets the location of the assembly of the bot.</summary>
		public static FileInfo Location
		{
			get
			{
				return new FileInfo(typeof(Bot).Assembly.Location);
			}
		}

		public static ILog Log = LogManager.GetLogger(typeof(Bot));

		/// <summary>Creates a new instance of the bot.</summary>
		public Bot()
		{
			Logging.Setup();

			try
			{
				var sw = Stopwatch.StartNew();
				FieldInfo.Instance = FieldInfo.Load(Location.FullName + ".dat");
				Log.DebugFormat("Init of Field took {0:0.000} seconds.", sw.Elapsed.TotalSeconds);
				Turns = new TurnInfos();
				Scenarios = new IScenario[]
				{
					Scenario.Possession,
					Scenario.Default,
				};
			}
			catch (Exception x)
			{
				Log.Fatal("Constructor failed:", x);
				throw;
			}
			
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
				Log.Error("Action failed:", x);
			}
		}
	}
}
