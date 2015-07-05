using CloudBall.Engines.LostKeysUnited.Models;
using CloudBall.Engines.LostKeysUnited.Scenarios;
using log4net;
using System;
using System.IO;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited
{
	[BotName("Lost Keys United 6.1")]
	public class LostKeysUnited : Common.ITeam
	{
		/// <summary>Gets the location of the assembly of the bot.</summary>
		public static FileInfo Location
		{
			get
			{
				return new FileInfo(typeof(LostKeysUnited).Assembly.Location);
			}
		}

		public static ILog Log = LogManager.GetLogger(
			((BotNameAttribute)(typeof(LostKeysUnited)
			.GetCustomAttributes(typeof(BotNameAttribute), false)[0]))
			.Name);

		/// <summary>Creates a new instance of the bot.</summary>
		public LostKeysUnited()
		{
			Logging.Setup();
			State = new GameState();
			Scenarios = new IScenario[]
			{
				Scenario.Defensive,
				Scenario.Default
			};
		}

		public IScenario[] Scenarios { get; protected set; }


		/// <summary>Gets the state of this game.</summary>
		public GameState State { get; protected set; }

		/// <summary>The central method that is used by the Game Engine to run the bot.</summary>
		public void Action(Common.Team myTeam, Common.Team enemyTeam, Common.Ball ball, Common.MatchInfo matchInfo)
		{
			try
			{
				var mapping = PlayerMapping.CreateForOwn(myTeam.Players, enemyTeam.Players);
				State.Add(myTeam, enemyTeam, ball, matchInfo);

				var fallen = State.Current.Players.Where(p => p.FallenTimer != 0).ToList();
				var tackle = State.Current.Players.Where(p => p.TackleTimer != 0).ToList();
		
				var queue = new PlayerQueue(State.Current.OwnPlayers);
				foreach (var scenario in Scenarios)
				{
					if (scenario.Apply(State, queue)) { break; }
				}
				mapping.Apply(queue.Actions);

			}
			catch (Exception x)
			{
				Log.Error(State.GetErrorMessage(), x);
			}
			finally
			{
				State.Current.Finish();
			}
		}
	}
}
