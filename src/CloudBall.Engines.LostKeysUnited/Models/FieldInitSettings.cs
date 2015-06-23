using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBall.Engines.LostKeysUnited
{
	public class FieldInitSettings
	{
		public void Calculate()
		{
			PassDistances = new List<Distance>();

			var distance = BallInfo.PowerToSpeed * PassPower;
			PassDistances.Add(Distance.Create(distance));

			while (PassDistances.Average(d => d.Value) > BallInfo.PowerToSpeed * AveragePassPower)
			{
				distance *= BallInfo.Accelaration;
				PassDistances.Add(Distance.Create(distance));
			}
			MaximumPassDistance = Distance.Create(PassDistances.Sum(d => d.Value));
		}

		public int ZoneSize { get; set; }
		public float PassPower { get; set; }
		public float AveragePassPower { get; set; }

		public int MaximumPassDuration { get { return PassDistances.Count; } }
		public List<Distance> PassDistances { get; private set; }

		public Distance MinimumPassDistance { get; set; }
		public Distance MaximumPassDistance { get; private set; }

		public Distance MaximumShootDistance { get; set; }
		public float MaximumProgressLoss { get; set; }
	}
}
