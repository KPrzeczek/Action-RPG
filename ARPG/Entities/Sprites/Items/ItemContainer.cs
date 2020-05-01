using ARPG.Entities.Sprites.Items.Food;
using ARPG.Entities.Sprites.Items.Misc;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ARPG.Entities.Sprites.Items
{
	public static class ItemContainer
	{
		private static readonly List<Item> itemTypes;

		static ItemContainer()
		{
			itemTypes = new List<Item>();
		}

		public static void GenerateItemDefinitions(ContentManager Content)
		{
			itemTypes.Add(new NullItem(Content.Load<Texture2D>("inventory/items/misc/null_item"), "NULL", 0));

			#region Equipment Definitions

			#endregion

			#region Food Definitions

			itemTypes.Add(new AppleItem(Content.Load<Texture2D>("inventory/items/food/apple"), "Apple", 1));

			#endregion

			#region Weapon Definitions

			#endregion

			#region Miscellaneous Definitions

			

			#endregion
		}

		#region Item Management

		public static Item GetItemViaID(int ID)
		{
			Item item = itemTypes.Find(c => c.ID == ID);

			// Check if item exists
			if(item != null)
			{
				// Return item
				return item;
			}

			// Item doesn't exist
			throw new Exception("Null Item ID.");
		}

		#endregion
	}
}
