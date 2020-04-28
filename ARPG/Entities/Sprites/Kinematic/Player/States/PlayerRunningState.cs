using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Entities.Sprites.Kinematic.Player.States
{
	public class PlayerRunningState : PlayerState
	{
		public PlayerRunningState(Player player) : base(player)
		{
		}

		public override void Enter()
		{
			player.Stats.SpeedModifier = 2f;
			player.AnimationManager.CurrentAnimation.FrameSpeed = 0.05f;
		}

		public override void Exit()
		{
			player.Stats.SpeedModifier = 1f;
			player.AnimationManager.CurrentAnimation.FrameSpeed = 0.1f;
		}

		public override void Update(float deltaTime)
		{
			if(!Keyboard.GetState().IsKeyDown(Keys.LeftShift))
			{
				player.RequestState(new PlayerWalkState(player));
			}
		}

		public override void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
		}
	}
}
