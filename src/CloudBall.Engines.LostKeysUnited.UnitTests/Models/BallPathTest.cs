using CloudBall.Engines.LostKeysUnited.Models;
using Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CloudBall.Engines.LostKeysUnited.UnitTests
{
	[TestFixture]
	public class BallPathTest
	{
		[Test]
		public void Accelaration_None_0f9930925()
		{
			var exp = Constants.BallSlowDownFactor;
			Assert.AreEqual(exp, BallPath.Accelaration);
		}

		[Test]
		public void Accelaration_BallHalfTime_0f5()
		{
			var exp = 0.5f;
			var act = 1f;

			for (var i = 0; i < Constants.BallHalfTime; i++)
			{
				act *= BallPath.Accelaration;
			}
			Assert.AreEqual(exp, act, 0.00001f);
		}

		[Test]
		public void Create_GoalOwn_After47Turns()
		{
			var ball = new Position(400, 400);
			var velo = new Velocity(-10, 1.9f);

			var act = BallPath.Create(ball, velo, 0, 1000);

			var expEnding = BallPath.Ending.GoalOwn;
			var expLength = 47;

			Assert.AreEqual(expEnding, act.End);
			Assert.AreEqual(expLength, act.Count);
		}

		[Test]
		public void Create_GoalOther_After58Turns()
		{
			var ball = new Position(1400, 600);
			var velo = new Velocity(11, 0);

			var act = BallPath.Create(ball, velo, 0, 1000);

			var expEnding = BallPath.Ending.GoalOther;
			var expLength = 58;

			Assert.AreEqual(expEnding, act.End);
			Assert.AreEqual(expLength, act.Count);
		}

		[Test]
		public void Create_EndOfGame_After10Turns()
		{
			var ball = new Position(1400, 600);
			var velo = new Velocity(11, 0);

			var act = BallPath.Create(ball, velo, 0,10);

			var expEnding = BallPath.Ending.EndOfGame;
			var expLength = 10;

			Assert.AreEqual(expEnding, act.End);
			Assert.AreEqual(expLength, act.Count);
		}

		[Test]
		public void Performance_Rnd()
		{
			var rnd = new Random(17);
			var tests = 10000;
			var length = 500;

			var balls = new List<Position>();
			var velos = new List<Velocity>();

			for (var i = 0; i < tests; i++)
			{
				var bX = (float)rnd.NextDouble() * Game.Field.MaximumX;
				var bY = (float)rnd.NextDouble() * Game.Field.MaximumX;

				var vX = (float)rnd.NextDouble();
				var vY = (float)rnd.NextDouble();

				balls.Add(new Position(bX, bY));
				velos.Add(new Velocity(vX, vY).Scale(5 + 7 * rnd.NextDouble()));
			}

			var sw = Stopwatch.StartNew();
			for (var i = 0; i < tests; i++)
			{
				var path = BallPath.Create(balls[i], velos[i], 0, length);
			}
			sw.Stop();
			Console.WriteLine("Elapsed: {0}, {1:0.000} ms/path", sw.Elapsed, sw.Elapsed.TotalMilliseconds / tests);
		}
	}
}
