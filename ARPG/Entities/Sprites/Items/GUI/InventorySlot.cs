using ARPG.GUI.Interactable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ARPG.Entities.Sprites.Items.Gui
{
	public class InventorySlot : GuiButton
	{
		private ItemStack itemStack;

		public InventorySlot(Texture2D tex, Texture2D hoverTex, Texture2D pressTex) : base(tex, hoverTex, pressTex)
		{
			Origin = Vector2.Zero;
			Scale = 4f;
			OnClick += InventorySlot_OnClick;
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);

			/*
			if(Pressed)
			{
				texture = pressTexture;
			}
			*/
		}

		public void AddItem(ItemStack item)
		{
			// If the item in the slot is the same
			if(itemStack.Item.GetType() == item.Item.GetType())
			{
				itemStack.ItemCount += item.ItemCount;
				return;
			}

			// Else just set the item
			itemStack = item;

			// TODO: Place other item into mouse slot (if it exists)
		}

		public void Clear()
		{
			itemStack = null;
		}

		public void UseItem()
		{
			itemStack?.Item?.OnUse();
		}

		private void InventorySlot_OnClick(object sender, System.EventArgs e)
		{
		}
	}
}
