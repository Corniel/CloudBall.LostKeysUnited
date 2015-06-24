using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBall.Engines.LostKeysUnited.UnitTests
{
	[TestFixture]
	public class ShootSimulatorTest
	{
		[Test]
		public void Shoot_Power10_()
		{
			var sims = 1000;
			var simulator = new ShootSimulator();
			var source = new Position(0, 0);
			var target = new Position(100, 0);

			var min_power = 5f;
			var max_power = 10.01f;
			var stp_power = 0.1f;

			var min_delta = 0.75f;
			var max_delta = 0.951f;
			var stp_delta = 0.05f;

			var p_size = 1 + (int)((max_power-min_power) / stp_power);
			var d_size = 1 + (int)((max_delta-min_delta) / stp_delta);

			var result = new Double[p_size, d_size];

			for (var p = 0; p < p_size; p++)
			{
				var power = p * stp_power + min_power;

				for (var d = 0; d < d_size; d++)
				{
					var delta = d * stp_delta + min_delta;

					var tests = new Dictionary<double, int>();

					for (var sim = 0; sim < sims; sim++)
					{
						var act = simulator.Shoot(source, target, power);

						var a = Math.Atan2(act.Y, act.X);
						if (!tests.ContainsKey(a))
						{
							tests[a] = 1;
						}
						else
						{
							tests[a]++;
						}
					}

					var safe = (int)(sims * delta);
					var sum = 0;

					foreach (var kvp in tests.OrderBy(r => r.Key))
					{
						sum += kvp.Value;
						if (sum >= safe)
						{
							result[p, d] = kvp.Key;
							break;
						}
					}

				}
			}

			WriteResult(result);
		}

		private void WriteResult(double[,] result)
		{
			var p_size = result.GetLength(0);
			var d_size = result.GetLength(1);

			using (var writer = new StreamWriter("../ShootingAccuracy.cs"))
			{
				writer.WriteLine("{");
				for (var p = 0; p < p_size; p++)
				{
					writer.Write("{{ {0:0.000}f /*  {1:00.0}° */", result[p, 0], ToAngle(result[p, 0]));
					for (var d = 1; d < d_size; d++)
					{	
						writer.Write(", {0:0.000}f /*  {1:00.0}° */", result[p, d], ToAngle(result[p, d]));
					}
					writer.WriteLine(" },");
				}
				writer.WriteLine("};");
			}
		}

		private Double ToAngle(Double theta)
		{
			return theta * 360.0 / Math.PI;
		}
	}
}
