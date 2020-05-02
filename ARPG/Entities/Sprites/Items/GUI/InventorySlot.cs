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
		public int ItemCount
		{
			get => ItemStack.ItemCount;
			set => ItemStack.ItemCount = value;
		}
		public Item Item
		{
			get => ItemStack.Item;
			set => ItemStack.Item = value;
		}

		public InventorySlot(Texture2D tex, Texture2D hoverTex, Texture2D pressTex) : base(tex, hoverTex, pressTex)
		{
			ItemStack = new ItemStack();
			Origin = Vector2.Zero;
			Scale = 4f;
			OnClick += InventorySlot_OnClick;
		}

		private void InventorySlot_OnClick(object sender, EventArgs e)
		{
			if(MouseSlot.Item == null)
			{
				if(Item != null)
				{
					// The Mouse doesn't contain anything but the slot does
					MouseSlot.Item = Item;
					MouseSlot.ItemCount = ItemCount;
					Clear();

					return;
				}
				else
				{
					// Both are null, no point in doing anything
					return;
				}
			}
			else
			{
				if(Item == null)
				{
					// Only the mouse contains the item
					Item = MouseSlot.Item;
					ItemCount = MouseSlot.ItemCount;
					MouseSlot.Clear();

					return;
				}
				else
				{
					// Mouse and the slot contain items
					Item item = MouseSlot.Item;
					int count = MouseSlot.ItemCount;

					MouseSlot.Item = Item;
					MouseSlot.ItemCount = count;

					Item = item;
					ItemCount = count;

					return;
				}
			}
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);

			if(isHovering && Item != null)
			{
				ItemHoverInfo.ItemInfo = Item.Clone() as Item;
			}
			else
			{
				ItemHoverInfo.ItemInfo = null;
			}
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
					0.91f
				);
			}
		}

		public void AddItem(ItemStack item)
		{
			// TODO: Check if item exists in slot, if so, add it to the itemCount rather than replacing it

			ItemStack = item;
		}

		public void Clear()
		{
			ItemStack = new ItemStack();
		}

		public object Clone()
		{
			return new InventorySlot(texture, hoverTexture, pressTexture) as InventorySlot;
		}
	}
}
