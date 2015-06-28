using CloudBall.Engines.LostKeysUnited.Models;
using log4net;
using System;
using System.IO;

namespace CloudBall.Engines.LostKeysUnited
{
	[BotName("Lost Keys United 5.2")]
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

		public static ILog Log = LogManager.GetLogger(typeof(LostKeysUnited));

		/// <summary>Creates a new instance of the bot.</summary>
		public LostKeysUnited()
		{
			Logging.Setup();
			History = new GameState();
		}

		/// <summary>Gets the state of this game.</summary>
		public GameState History { get; protected set; }

		//public IScenario[] Scenarios { get; private set; }

		/// <summary>The central method that is used by the Game Engine to run the bot.</summary>
		public void Action(Common.Team myTeam, Common.Team enemyTeam, Common.Ball ball, Common.MatchInfo matchInfo)
		{
			try
			{
				var mapping = PlayerMapping.CreateForOwn(myTeam.Players, enemyTeam.Players);
				History.Add(myTeam, enemyTeam, ball, matchInfo);

				var queue = new PlayerQueue(History.Current.OwnPlayers);
				Scenario.Default.Apply(History, queue);
				mapping.Apply(queue.Actions);

			}
			catch (Exception x)
			{
				Log.Error(History.GetErrorMessage(), x);
			}
			finally
			{
				History.Current.Finish();
			}
		}
	}
}
