using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited
{
	public class CatchUp : Dictionary<PlayerInfo, TurnPosition>
	{
		[Flags]
		public enum ResultType
		{
			None = 0,
			Own = 1,
			Other = 2,
			Both = Own | Other,
		}

		public ResultType Result
		{
			get
			{
				if (Count == 0) { return ResultType.None; }

				var own = this.Where(kvp => kvp.Key.Team == TeamType.Own).OrderBy(kvp => kvp.Value.Turn).FirstOrDefault();
				var other = this.Where(kvp => kvp.Key.Team == TeamType.Other).OrderBy(kvp => kvp.Value.Turn).FirstOrDefault();

				var tOwn = own.Key == null ? Int32.MaxValue : own.Value.Turn;
				var tOther = other.Key == null ? Int32.MaxValue : other.Value.Turn;

				if (tOwn == tOther) { return tOwn == Int32.MaxValue ? ResultType.None : ResultType.Both; }
				return tOwn < tOther ? ResultType.Own : ResultType.Other;
			}
		}

		public IEnumerable<KeyValuePair<PlayerInfo, TurnPosition>> GetOwn()
		{
			return this.Where(kvp => kvp.Key.Team == TeamType.Own && !kvp.Value.IsUnknown).OrderBy(kvp => kvp.Value.Turn);
		}

		public KeyValuePair<PlayerInfo, TurnPosition> Catcher
		{
			get
			{
				return this.OrderBy(kvp => kvp.Value.Turn).FirstOrDefault();
			}
		}
	}
}
