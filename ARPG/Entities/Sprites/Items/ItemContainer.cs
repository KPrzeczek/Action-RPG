using ARPG.Entities.Sprites.Items.Food;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ARPG.Entities.Sprites.Items
{
	public static class ItemContainer
	{
		private static readonly Dictionary<int, Item> itemTypes; // int = itemID, Item = item

		static ItemContainer()
		{
			itemTypes = new Dictionary<int, Item>();
		}

		public static void GenerateItemDefinitions(ContentManager Content)
		{
			#region Equipment Definitions

			#endregion

			#region Food Definitions

			AddItem(0, new AppleItem(Content.Load<Texture2D>("inventory/items/food/apple"), "Apple"));

			#endregion

			#region Weapon Definitions

			#endregion

			#region Miscellaneous Definitions

			#endregion

		}

		#region Item Management

		public static Item GetItem(int ID)
		{
			// Try to find item of ID
			if(itemTypes.ContainsKey(ID))
			{
				// Return item
				Item item = itemTypes[ID];
				return item;
			}

			// Item doesn't exist
			throw new Exception("Null Item ID.");
		}

		private static void AddItem(int ID, Item item)
		{
			// Only add the item ID doesn't exist
			if(!(itemTypes.ContainsKey(ID)))
			{
				itemTypes.Add(ID, item);
				return;
			}

			throw new Exception("Item ID is already taken.");
		}

		#endregion
	}
}
