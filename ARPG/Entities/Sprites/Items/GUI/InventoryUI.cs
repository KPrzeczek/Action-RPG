using ARPG.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*
 * Handles inventory UI
 */

namespace ARPG.Entities.Sprites.Items.GUI
{
	public class InventoryUI : IGuiComponent
	{
		private Inventory inventory;
		private InventorySlot[] slots;
		private MouseSlot mouseSlot;

		private bool opened = false;
		private bool canOpen = true;

		public InventoryUI(Inventory inv, InventorySlot slotPrefab)
		{
			inventory = inv;
			inventory.OnItemChangedCallback += UpdateUI;

			// Initialize Slots
			slots = new InventorySlot[inventory.Space];
			mouseSlot = new MouseSlot();

			// Populate Slots
			for(int ii = 0; ii < slots.Length; ii++)
			{
				var clone = slotPrefab.Clone() as InventorySlot;
				slots[ii] = clone;
			}

			// Set Slot Positions
			int xx = 5, yy = 5;
			int diff = 18;

			for(int ii = 0; ii < slots.Length; ii++)
			{
				float scale = slots[ii].Scale;
				slots[ii].Position = new Vector2(xx * scale, yy * scale);

				xx += diff;

				if(xx > (inventory.Width * diff))
				{
					xx = 5;
					yy += diff;
				}
			}

			slots[0].Item = ItemContainer.GetItemViaID(1);
		}

		public void Update(float deltaTime)
		{
			#region Handle Opening

			if(Keyboard.GetState().IsKeyDown(Keys.E) && canOpen)
			{
				opened = !opened;
				canOpen = false;
			}

			if(!(Keyboard.GetState().IsKeyDown(Keys.E)))
			{
				canOpen = true;
			}

			#endregion

			if(!opened)
				return;

			foreach(var slot in slots)
				slot.Update(deltaTime);

			mouseSlot.Update(deltaTime);
		}

		public void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
			if(!opened)
				return;

			foreach(var slot in slots)
				slot.Draw(deltaTime, spriteBatch);

			mouseSlot.Draw(deltaTime, spriteBatch);
		}

		private void UpdateUI()
		{
			for(int i = 0; i < slots.Length; i++)
			{
				if(i < inventory.items.Count)
				{
					slots[i].AddItem(inventory.items[i]);
				}
				else
				{
					slots[i].Clear();
				}
			}
		}
	}
}
