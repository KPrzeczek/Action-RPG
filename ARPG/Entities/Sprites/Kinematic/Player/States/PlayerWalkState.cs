using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Entities.Sprites.Kinematic.Player.States
{
	public class PlayerWalkState : PlayerState
	{
		public PlayerWalkState(Player player) : base(player)
		{
		}

		public override void Enter()
		{
			player.AnimationManager.Play();
		}

		public override void Exit()
		{
		}

		public override void Update(float deltaTime)
		{
			if(Keyboard.GetState().IsKeyDown(player.Input.Run))
			{
				player.RequestState(new PlayerRunningState(player));
			}
		}

		public override void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
		}
	}
}
