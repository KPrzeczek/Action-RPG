using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Util.States
{
	public class StateMachine
	{
		public IState CurrentState;

		public StateMachine(IState startState)
		{
			ChangeState(startState);
		}

		public void Update(float deltaTime)
		{
			CurrentState?.Update(deltaTime);
		}

		public void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
			CurrentState?.Draw(deltaTime, spriteBatch);
		}

		public void ChangeState(IState state)
		{
			CurrentState?.Exit();
			CurrentState = state;
			CurrentState?.Enter();
		}
	}
}
