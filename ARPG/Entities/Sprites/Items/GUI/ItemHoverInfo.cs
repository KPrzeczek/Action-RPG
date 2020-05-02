using ARPG.GUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ARPG.Entities.Sprites.Items.GUI
{
	public class ItemHoverInfo : Entity
	{
		private Vector2 position;

		private SpriteFont textFont;
		private GuiNineSliceGrid textBackground;

		public static Item ItemInfo { get; set; }

		public ItemHoverInfo(Texture2D nineSliceGrid, SpriteFont font)
		{
			textFont = font;
			textBackground = new GuiNineSliceGrid(nineSliceGrid);

			textBackground.Width = 5;
		}

		public override void Update(float deltaTime)
		{
			position = MouseSlot.Position + (Vector2.One * 10);
		}

		public override void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
			//textBackground.Width = (int)textFont.MeasureString(ItemInfo?.Name).X;
			//textBackground.Height = (int)textFont.MeasureString(ItemInfo?.Name).Y;

			textBackground.Position = position;

			textBackground.Draw(deltaTime, spriteBatch);
		}
	}
}
