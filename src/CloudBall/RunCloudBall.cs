using CloudBall.Engines;
using Common;
using System;
using System.Collections.Generic;

namespace CloudBall
{
#if WINDOWS || XBOX
	static class RunCloudBall
	{
		/// <summary>The main entry point for the application.</summary>
		[STAThread]
		static void Main(string[] args)
		{
			var teams = new List<ITeam>()
			{
				new CloudBall.Engines.LostKeysUnited.LostKeysUnited(),
				//TeamFactory.Load(@"*\Arena\Engines\Wolkenhondjes_6.1.dll"),
				new SimpleStart(),
			};

			using (Client.Client client = new Client.Client(teams[0], teams[2]))
			{
				client.Run();
			}
		}
	}
#endif
}


