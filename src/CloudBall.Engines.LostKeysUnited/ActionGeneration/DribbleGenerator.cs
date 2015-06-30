using CloudBall.Engines.LostKeysUnited.IActions;
using CloudBall.Engines.LostKeysUnited.Models;
using System;
using System.Linq;

namespace CloudBall.Engines.LostKeysUnited.ActionGeneration
{
	/// <summary>Generates options based on dribbling with the ball.</summary>
	/// <remarks>
	/// Don't walk back.
	///
	/// |  / _
	/// | /_~
	///[o]----
	/// |\~_
	/// | \ ~
	/// 
	/// </remarks>
	public class DribbleGenerator : IActionGenerator
	{
		public static readonly Velocity[] WalkingDirections = new Velocity[]
		{
			new Velocity(10, -90),
			new Velocity(30, -60),
			new Velocity(60, -30),
			new Velocity(99, +00),
			new Velocity(60, +30),
			new Velocity(30, +60),
			new Velocity(10, +90),
		};

		public const int MinimumDribbleLength = 3;
		public const int MaximumDribbleLength = 100;

		public void Generate(PlayerInfo owner, GameState state, ActionCandidates candidates)
		{
			if (!owner.IsBallOwner || owner.CanBetTackled.Any()) { return; }

			foreach (var direction in WalkingDirections)
			{
				var target = owner.Position + direction;

				// Don't try to leave the field.
				if(Game.Field.IsOnField(target))
				{
					var dribble = PlayerPath.Create(owner, target, MaximumDribbleLength, Distance.Tackle);
					var catchUp = dribble.GetCatchUps(state.Current.OtherPlayers).FirstOrDefault();

					// don't start (too) short walks.
					if(catchUp == null || catchUp.Turn >= MinimumDribbleLength)
					{
						if (catchUp != null) { target = dribble[catchUp.Turn - 2]; }

						var action = Actions.Move(owner, target);
						var length = catchUp == null ? dribble.Count : catchUp.Turn - 1;
						candidates.Add(Evaluator.GetPositionImprovement(owner, target, length), action);
					}
				}
			}
		}
	}
}
