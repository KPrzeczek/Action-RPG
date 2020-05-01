using System;
using ARPG.Game_States;
using ARPG.GUI.Interactable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ARPG.Entities.Sprites.Items.GUI
{
	public class InventorySlot : GuiButton, ICloneable
	{
		public ItemStack ItemStack { get; private set; }

		public InventorySlot(Texture2D tex, Texture2D hoverTex, Texture2D pressTex) : base(tex, hoverTex, pressTex)
		{
			ItemStack = new ItemStack();
			Origin = Vector2.Zero;
			Scale = 4f;
			OnClick += InventorySlot_OnClick;
		}

		private void InventorySlot_OnClick(object sender, EventArgs e)
		{
			ItemStack.Item?.OnUse();
		}

		public override void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
			base.Draw(deltaTime, spriteBatch);

			// Draw Item
			if(ItemStack?.Item?.Icon != null)
			{
				spriteBatch.Draw(
					ItemStack.Item.Icon,
					Position + new Vector2(
						texture.Width / 2f,
						texture.Height / 2f
					) * 4,
					null,
					Color.White,
					0f,
					new Vector2(
						ItemStack.Item.Icon.Width / 2f,
						ItemStack.Item.Icon.Height / 2f
					),
					(Scale / 4) * 3,
					SpriteEffects.None,
					0.93f
				);
			}
		}

		// Same as the SetItem function but also checks whether it can add to the stack
		public void AddItem(ItemStack item)
		{
			// TODO: Check if item exists in slot, if so, add it to the itemCount rather than replacing it

			ItemStack = item;
		}
		
		// Directly sets the item in the slot
		public void SetItem(Item item)
		{
			ItemStack.Item = item;
		}

		// Clears the slot of any items
		public void Clear()
		{
			ItemStack = null;
		}

		// Uses the item in the slot
		public void UseItem()
		{
			ItemStack?.Item?.OnUse();
		}

		public object Clone()
		{
			return this.MemberwiseClone() as InventorySlot;
		}
	}
}
