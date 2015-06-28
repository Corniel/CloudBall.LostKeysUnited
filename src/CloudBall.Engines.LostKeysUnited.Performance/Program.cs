using CloudBall.Engines.LostKeysUnited.UnitTests;

namespace CloudBall.Engines.LostKeysUnited.Performance
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var test = new BallPathTest();
			test.Performance_Rnd();
		}
	}
}
