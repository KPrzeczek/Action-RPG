using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ARPG.Entities.Sprites.Items.GUI
{
	public class MouseSlot : Entity
	{
		public static Vector2 Position;

		public static ItemStack ItemStack { get; private set; }
		public static int ItemCount
		{
			get => ItemStack.ItemCount;
			set => ItemStack.ItemCount = value;
		}
		public static Item Item
		{
			get => ItemStack.Item;
			set => ItemStack.Item = value;
		}

		public MouseSlot()
		{
			ItemStack = new ItemStack();
		}

		public override void Update(float deltaTime)
		{
			Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
		}

		public override void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
			if(ItemStack?.Item?.Icon != null)
			{
				spriteBatch.Draw(
					ItemStack.Item.Icon,
					Position,
					null,
					Color.White,
					0f,
					Vector2.Zero,
					3f,
					SpriteEffects.None,
					0.925f
				);
			}
		}

		public static void Clear()
		{
			ItemStack = new ItemStack();
		}

		public static void UseItem()
		{
			ItemStack?.Item?.OnUse();
		}
	}
}
