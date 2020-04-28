using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPG.Util.States;
using Microsoft.Xna.Framework.Graphics;

namespace ARPG.Entities.Sprites.Kinematic.Player.States
{
	public abstract class PlayerState : IState
	{
		protected Player player;

		public PlayerState(Player player)
		{
			this.player = player;
		}

		public abstract void Enter();
		public abstract void Exit();

		public abstract void Update(float deltaTime);
		public abstract void Draw(float deltaTime, SpriteBatch spriteBatch);
	}
}
