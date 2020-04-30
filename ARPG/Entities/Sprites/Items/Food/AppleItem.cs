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
			Scale = 1 / 2f;
		}

		public override void OnUse()
		{
		}
	}
}
