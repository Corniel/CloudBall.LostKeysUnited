using CloudBall.Engines.LostKeysUnited.Models;
using NUnit.Framework;

namespace CloudBall.Engines.LostKeysUnited.UnitTests.Models
{
	[TestFixture]
	public class PlayerQueueTest
	{
		[Test]
		public void Dequeue_ActionsNone_NothingChanges()
		{
			var player = new PlayerInfo() { Id = 4 };
			var queue = new PlayerQueue(player);

			Assert.IsFalse(queue.Dequeue(Actions.None), "Nothing should be de-queued.");
			Assert.AreEqual(1, queue.Count, "number of elements should still be 1.");
			Assert.AreEqual(0, queue.Actions.Count, "Should contain no actions.");
		}

		[Test]
		public void Dequeue_ActionsWait4_AddsOneAction()
		{
			var player = new PlayerInfo() { Id = 4 };
			var queue = new PlayerQueue(player);

			Assert.IsTrue(queue.Dequeue(Actions.Wait(player)), "Player 4 should be de-queued.");
			Assert.AreEqual(0, queue.Count, "number of elements should be 0.");
			Assert.AreEqual(1, queue.Actions.Count, "Should contain 1 action.");
		}

		[Test]
		public void Dequeue_ActionsWait3_NothingChanges()
		{
			var player = new PlayerInfo() { Id = 4 };
			var other = new PlayerInfo() { Id = 3 };
			var queue = new PlayerQueue(other);

			Assert.IsFalse(queue.Dequeue(Actions.None), "Nothing should be de-queued.");
			Assert.AreEqual(1, queue.Count, "number of elements should still be 1.");
			Assert.AreEqual(0, queue.Actions.Count, "Should contain no actions.");
		}
	}
}
