using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ARPG.Entities.Sprites.Items.Gui
{
	public class InventoryUI : IComponent
	{
		private Inventory inventory;
		private InventorySlot[] slots;

		private bool opened;
		private bool canOpen;

		public InventoryUI(Inventory inv, InventorySlot slotPrefab)
		{
			inventory = inv;
			inventory.OnItemChangedCallback += UpdateUI;

			slots = new InventorySlot[]
			{
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab,
				slotPrefab
			};
		}

		public void Update(float deltaTime)
		{
			foreach(var slot in slots)
				slot.Update(deltaTime);

			#region Open Handling

			bool isKeyDown(Keys key) => Keyboard.GetState().IsKeyDown(key);

			if(isKeyDown(Keys.E) && canOpen)
			{
				opened = !opened;
				canOpen = false;
			}

			if(!(isKeyDown(Keys.E)))
			{
				canOpen = true;
			}

			#endregion
		}

		public void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
			foreach(var slot in slots)
				slot.Draw(deltaTime, spriteBatch);

			slots[0].Draw(deltaTime, spriteBatch);
		}

		private void UpdateUI()
		{
			for(int ii = 0; ii < slots.Length; ii++)
			{
				if(ii < inventory.Items.Count)
				{
					slots[ii].AddItem(inventory.Items[ii]);
				}
				else
				{
					slots[ii].Clear();
				}
			}
		}
	}
}
