using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace CloudBall.Engines.LostKeysUnited
{
	public static class Logging
	{
		public static void Setup()
		{
			Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

			var patternLayout = new PatternLayout();
			patternLayout.ConversionPattern = "%date{yyyy-MM-dd HH:mm:ss.fff} %-6level  %message%newline";
			patternLayout.ActivateOptions();

			var roller = new RollingFileAppender();
			roller.AppendToFile = true;
			roller.File = LostKeysUnited.Location.FullName + ".log";
			roller.Layout = patternLayout;
			roller.MaxSizeRollBackups = 5;
			roller.MaximumFileSize = "1GB";
			roller.RollingStyle = RollingFileAppender.RollingMode.Size;
			roller.StaticLogFileName = true;
			roller.ActivateOptions();
			hierarchy.Root.AddAppender(roller);
#if DEBUG
			hierarchy.Root.Level = Level.Debug;
#else
			hierarchy.Root.Level = Level.Error;
#endif
			hierarchy.Configured = true;
		}
	}
}
