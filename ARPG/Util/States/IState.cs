using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Util.States
{
	public interface IState
	{
		void Enter();
		void Exit();

		void Update(float deltaTime);
		void Draw(float deltaTime, SpriteBatch spriteBatch);
	}
}
