using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Entities.Sprites.Items.Gui
{
	public class Inventory
	{
		public delegate void OnItemChanged();
		public OnItemChanged OnItemChangedCallback;

		public List<ItemStack> Items = new List<ItemStack>();
		public int Space = 36;
		public int AvailableSpace
		{
			get
			{
				return (Space - Items.Count);
			}
		}

		public bool Add(ItemStack item)
		{
			// Check if out of space
			if(Items.Count >= Space)
			{
				throw new Exception("Not Enough Inventory Space");
				return false;
			}

			// Add Item
			Items.Add(item);

			// Trigger Callback
			if(OnItemChangedCallback != null)
			{
				OnItemChangedCallback.Invoke();
			}

			return true;
		}

		public void Remove(ItemStack item)
		{
			// Remove Item
			Items.Remove(item);

			// Trigger Callback
			if(OnItemChangedCallback != null)
				OnItemChangedCallback.Invoke();
		}
	}
}
