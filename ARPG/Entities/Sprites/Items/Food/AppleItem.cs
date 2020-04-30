using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Entities.Sprites.Items.Food
{
	public class AppleItem : Item
	{
		public AppleItem(Texture2D tex, string name) : base(tex, name)
		{
			Position = new Vector2(100, 100);
		}

		public override void Update(float deltaTime)
		{
			if(Keyboard.GetState().IsKeyDown(Keys.H))
			{
				Scale -= 0.01f;
			}
			if(Keyboard.GetState().IsKeyDown(Keys.J))
			{
				Scale += 0.01f;
			}

			base.Update(deltaTime);
		}

		public override void OnUse()
		{
			Console.WriteLine("Apple Used!");
		}
	}
}
