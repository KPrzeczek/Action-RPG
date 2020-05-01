using System;
using System.Collections.Generic;

/*
 * Handles inventory logic
 */

namespace ARPG.Entities.Sprites.Items.GUI
{
	public class Inventory
	{
		public delegate void OnItemChanged();
		public OnItemChanged OnItemChangedCallback;

		public int Width;
		public int Height;
		public int Space
		{
			get
			{
				return (Width * Height);
			}
		}

		public List<ItemStack> items = new List<ItemStack>();

		public Inventory(int width, int height)
		{
			Width = width;
			Height = height;
		}

		#region Management

		// While I can just directly set the items in a slot, this is also here for utility

		public bool Add(ItemStack itemStack)
		{
			if(items.Count >= Space)
			{
				throw new Exception("Not enough space in inventory!");
				return false;
			}

			items.Add(itemStack);

			if(OnItemChangedCallback != null)
			{
				OnItemChangedCallback.Invoke();
			}

			return true;
		}

		public void Remove(ItemStack itemStack)
		{
			items.Remove(itemStack);

			if(OnItemChangedCallback != null)
			{
				OnItemChangedCallback.Invoke();
			}
		}

		#endregion
	}
}
